using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItemShipmentDocuments;

public interface IUpdateWorklistToBioHubItemShipmentDocumentsHandler
{
    Task<Either<UpdateWorklistToBioHubItemShipmentDocumentsCommandResponse, Errors>> Handle(UpdateWorklistToBioHubItemShipmentDocumentsCommand command, CancellationToken cancellationToken);
}

public class UpdateWorklistToBioHubItemShipmentDocumentsHandler : IUpdateWorklistToBioHubItemShipmentDocumentsHandler
{
    private readonly ILogger<UpdateWorklistToBioHubItemShipmentDocumentsHandler> _logger;
    private readonly UpdateWorklistToBioHubItemShipmentDocumentsCommandValidator _validator;
    private readonly IUpdateWorklistToBioHubItemShipmentDocumentsMapper _mapper;
    private readonly IWorklistToBioHubItemWriteRepository _writeRepository;
    private readonly IWorklistToBioHubEngine _worklistToBioHubEngine; 
    


    public UpdateWorklistToBioHubItemShipmentDocumentsHandler(
        ILogger<UpdateWorklistToBioHubItemShipmentDocumentsHandler> logger,
        UpdateWorklistToBioHubItemShipmentDocumentsCommandValidator validator,
        IUpdateWorklistToBioHubItemShipmentDocumentsMapper mapper,
        IWorklistToBioHubItemWriteRepository writeRepository,
        IWorklistToBioHubEngine worklistToBioHubEngine  
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _worklistToBioHubEngine = worklistToBioHubEngine;
    }

    public async Task<Either<UpdateWorklistToBioHubItemShipmentDocumentsCommandResponse, Errors>> Handle(
        UpdateWorklistToBioHubItemShipmentDocumentsCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)            
            return new(new Errors(ErrorType.RequestParsing, validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));

        IDbContextTransaction transaction = await _writeRepository.BeginTransactionAsync();

        try
        {
            WorklistToBioHubItem worklisttobiohubitem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (worklisttobiohubitem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
            }

            switch (command.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklisttobiohubitem.RequestInitiationFromLaboratoryId != command.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklisttobiohubitem.RequestInitiationToBioHubFacilityId != command.UserBioHubFacilityId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
                    }
                    break;

            }
            

            if (worklisttobiohubitem.Status < WorklistToBioHubStatus.WaitForPickUpCompleted)
            {
                return new(new Errors(ErrorType.Forbidden, $"Action forbidden"));
            }

            if (worklisttobiohubitem.IsPast == true && !command.UserPermissions.Contains(PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast))
            {
                return new(new Errors(ErrorType.Unauthorized, $"Unauthorized"));
            }

            if (worklisttobiohubitem.IsPast != true && !command.UserPermissions.Contains(PermissionNames.CanSubmitSMTA1ShipmentDocuments))
            {
                return new(new Errors(ErrorType.Unauthorized, $"Unauthorized"));
            }

            //if (worklisttobiohubitem.ReferenceId != command.ReferenceId)
            //{
            //    return new(new Errors(ErrorType.NotFound, $"The current page is not up to date - Please refresh the page"));

            //}

            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);

            await _worklistToBioHubEngine.UpdateShipmentDocuments(worklisttobiohubitem, moveToNextStatusCommand, transaction, cancellationToken);

            await transaction.CommitAsync();
            await transaction.DisposeAsync();
                       

            return new(new UpdateWorklistToBioHubItemShipmentDocumentsCommandResponse(worklisttobiohubitem.Id));

        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
            throw;
        }
    }

    private MoveToNextStatusToBioHubEngineCommand PrepareMoveToNextStatusCommand(UpdateWorklistToBioHubItemShipmentDocumentsCommand command)
    {
        MoveToNextStatusToBioHubEngineCommand moveToNextStatusCommand = new MoveToNextStatusToBioHubEngineCommand();
        moveToNextStatusCommand.Id = command.Id;
        moveToNextStatusCommand.DocumentTemplateFileType = command.DocumentTemplateFileType;
        moveToNextStatusCommand.File = command.File;
        moveToNextStatusCommand.UserId = command.UserId;
        moveToNextStatusCommand.ShipmentDocumentId = command.ShipmentDocumentId;
        moveToNextStatusCommand.ShipmentDocumentNewName = command.ShipmentDocumentNewName;
        moveToNextStatusCommand.ShipmentDocumentOperationType = command.ShipmentDocumentOperationType;
        moveToNextStatusCommand.ShipmentDocuments = command.ShipmentDocuments;       

        return moveToNextStatusCommand;
    }    
}