using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemBHFShipmentDocuments;

public interface IUpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler
{
    Task<Either<UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandResponse, Errors>> Handle(UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand command, CancellationToken cancellationToken);
}

public class UpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler : IUpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler
{
    private readonly ILogger<UpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler> _logger;
    private readonly UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandValidator _validator;
    private readonly IUpdateWorklistFromBioHubItemBHFShipmentDocumentsMapper _mapper;
    private readonly IWorklistFromBioHubItemWriteRepository _writeRepository;
    private readonly IWorklistFromBioHubEngine _worklistFromBioHubEngine;
    private readonly IWorklistFromBioHubHistoryItemWriteRepository _worklistFromBioHubHistoryItemWriteRepository;
    private readonly IWorklistFromBioHubEmailNotifier _worklistFromBioHubEmailNotifier;


    public UpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler(
        ILogger<UpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler> logger,
        UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandValidator validator,
        IUpdateWorklistFromBioHubItemBHFShipmentDocumentsMapper mapper,
        IWorklistFromBioHubItemWriteRepository writeRepository,
        IWorklistFromBioHubEngine worklistFromBioHubEngine,
        IWorklistFromBioHubHistoryItemWriteRepository worklistFromBioHubHistoryItemWriteRepository,
        IWorklistFromBioHubEmailNotifier worklistFromBioHubEmailNotifier
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _worklistFromBioHubEngine = worklistFromBioHubEngine;
        _worklistFromBioHubHistoryItemWriteRepository = worklistFromBioHubHistoryItemWriteRepository;
        _worklistFromBioHubEmailNotifier = worklistFromBioHubEmailNotifier;
    }

    public async Task<Either<UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandResponse, Errors>> Handle(
        UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            // return new(new Errors(validationResult));
            return new(new Errors(ErrorType.RequestParsing, validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));

        IDbContextTransaction transaction = await _writeRepository.BeginTransactionAsync();

        try
        {
            WorklistFromBioHubItem worklistfrombiohubitem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (worklistfrombiohubitem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
            }

            switch (command.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklistfrombiohubitem.RequestInitiationToLaboratoryId != command.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklistfrombiohubitem.RequestInitiationFromBioHubFacilityId != command.UserBioHubFacilityId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
                    }
                    break;

            }

            if (worklistfrombiohubitem.Status != command.CurrentStatus)
            {
                return new(new Errors(ErrorType.NotFound, $"Item Status mismatch with the current page status - Please refresh the page"));
            }

            if (worklistfrombiohubitem.Status < WorklistFromBioHubStatus.WaitForPickUpCompleted)
            {
                return new(new Errors(ErrorType.Forbidden, $"Action forbidden"));
            }

            if (worklistfrombiohubitem.IsPast == true && !command.UserPermissions.Contains(PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast))
            {
                return new(new Errors(ErrorType.Unauthorized, $"Unauthorized"));
            }

            if (worklistfrombiohubitem.IsPast != true && !command.UserPermissions.Contains(PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments))
            {
                return new(new Errors(ErrorType.Unauthorized, $"Unauthorized"));
            }

            //if (worklistfrombiohubitem.ReferenceId != command.ReferenceId)
            //{
            //    return new(new Errors(ErrorType.NotFound, $"The current page is not up to date - Please refresh the page"));
            //}          



            worklistfrombiohubitem = _mapper.Map(worklistfrombiohubitem, command);

            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);

            
           await _worklistFromBioHubEngine.UpdateBHFShipmentDocuments(worklistfrombiohubitem, moveToNextStatusCommand, transaction, cancellationToken);
           

            await transaction.CommitAsync();
            await transaction.DisposeAsync();


            return new(new UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandResponse(worklistfrombiohubitem.Id));

        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
            throw;
        }
    }

    private MoveToNextStatusFromBioHubEngineCommand PrepareMoveToNextStatusCommand(UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand command)
    {
        MoveToNextStatusFromBioHubEngineCommand moveToNextStatusCommand = new MoveToNextStatusFromBioHubEngineCommand();
        moveToNextStatusCommand.Id = command.Id;        
        moveToNextStatusCommand.File = command.File;
        moveToNextStatusCommand.UserId = command.UserId;
        moveToNextStatusCommand.DocumentTemplateFileType = command.DocumentTemplateFileType;
        moveToNextStatusCommand.ShipmentDocumentId = command.ShipmentDocumentId;
        moveToNextStatusCommand.ShipmentDocumentNewName = command.ShipmentDocumentNewName;
        moveToNextStatusCommand.ShipmentDocumentOperationType = command.ShipmentDocumentOperationType;
        moveToNextStatusCommand.BHFShipmentDocuments = command.BHFShipmentDocuments;     
      
        return moveToNextStatusCommand;
    }
    
}