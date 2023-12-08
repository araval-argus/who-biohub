using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;

public interface IUpdateSMTA1WorkflowItemHandler
{
    Task<Either<UpdateSMTA1WorkflowItemCommandResponse, Errors>> Handle(UpdateSMTA1WorkflowItemCommand command, CancellationToken cancellationToken);
}

public class UpdateSMTA1WorkflowItemHandler : IUpdateSMTA1WorkflowItemHandler
{
    private readonly ILogger<UpdateSMTA1WorkflowItemHandler> _logger;
    private readonly UpdateSMTA1WorkflowItemCommandValidator _validator;
    private readonly IUpdateSMTA1WorkflowItemMapper _mapper;
    private readonly ISMTA1WorkflowItemWriteRepository _writeRepository;
    private readonly ISMTA1WorkflowEngine _SMTA1WorkflowEngine;
    private readonly ISMTA1WorkflowHistoryItemWriteRepository _SMTA1WorkflowHistoryItemWriteRepository;
    private readonly ISMTA1WorkflowEmailNotifier _SMTA1WorkflowEmailNotifier;


    public UpdateSMTA1WorkflowItemHandler(
        ILogger<UpdateSMTA1WorkflowItemHandler> logger,
        UpdateSMTA1WorkflowItemCommandValidator validator,
        IUpdateSMTA1WorkflowItemMapper mapper,
        ISMTA1WorkflowItemWriteRepository writeRepository,
        ISMTA1WorkflowEngine SMTA1WorkflowEngine,
        ISMTA1WorkflowHistoryItemWriteRepository SMTA1WorkflowHistoryItemWriteRepository,
        ISMTA1WorkflowEmailNotifier SMTA1WorkflowEmailNotifier
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _SMTA1WorkflowEngine = SMTA1WorkflowEngine;
        _SMTA1WorkflowHistoryItemWriteRepository = SMTA1WorkflowHistoryItemWriteRepository;
        _SMTA1WorkflowEmailNotifier = SMTA1WorkflowEmailNotifier;

    }

    public async Task<Either<UpdateSMTA1WorkflowItemCommandResponse, Errors>> Handle(
        UpdateSMTA1WorkflowItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)            
            return new(new Errors(ErrorType.RequestParsing, validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));

        IDbContextTransaction transaction = null;

        try
        {
            SMTA1WorkflowItem SMTA1WorkflowItem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (SMTA1WorkflowItem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
            }

            switch (command.RoleType)
            {
                case RoleType.Laboratory:
                    if (SMTA1WorkflowItem.LaboratoryId != command.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
                    }
                    break;

            }

            if (SMTA1WorkflowItem.Status != command.CurrentStatus)
            {
                return new(new Errors(ErrorType.NotFound, $"Item Status mismatch with the current page status - Please refresh the page"));
            }

            if (SMTA1WorkflowItem.ReferenceId != command.ReferenceId)
            {
                return new(new Errors(ErrorType.NotFound, $"The current page is not up to date - Please refresh the page"));

            }

            var requiredPermission = StatusPermissionMapper.GetSMTA1WorkflowStatusPermission(SMTA1WorkflowItem.Status, PermissionType.Update, SMTA1WorkflowItem.IsPast);

            if (!command.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Action forbidden"));
            }

            transaction = await _writeRepository.BeginTransactionAsync(); ;

            if (command.IsSaveDraft != true)
            {
                await CreateHistoryItem(SMTA1WorkflowItem, cancellationToken, transaction);
            }

            SMTA1WorkflowItem = _mapper.Map(SMTA1WorkflowItem, command);

            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);

            if (command.LastSubmissionApproved == true || command.IsSaveDraft == true)
            {
                SMTA1WorkflowItem = await _SMTA1WorkflowEngine.MoveToNextStatusUponApproveOrSaveDraft(SMTA1WorkflowItem, moveToNextStatusCommand, cancellationToken, transaction);
            }
            else
            {
                SMTA1WorkflowItem = await _SMTA1WorkflowEngine.MoveToNextStatusUponReject(SMTA1WorkflowItem, moveToNextStatusCommand, cancellationToken, transaction);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            if (command.IsSaveDraft != true && SMTA1WorkflowItem.IsPast != true)
            {
                await _SMTA1WorkflowEmailNotifier.NotifyUsers(SMTA1WorkflowItem, cancellationToken);
            }

            return new(new UpdateSMTA1WorkflowItemCommandResponse(SMTA1WorkflowItem.Id));

        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            if (transaction != null)
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
            }
            return new(new Errors(ErrorType.Internal, $"Internal Server Error"));
        }
    }

    private MoveToNextStatusSMTA1WorkflowEngineCommand PrepareMoveToNextStatusCommand(UpdateSMTA1WorkflowItemCommand command)
    {
        MoveToNextStatusSMTA1WorkflowEngineCommand moveToNextStatusCommand = new MoveToNextStatusSMTA1WorkflowEngineCommand();
        moveToNextStatusCommand.Id = command.Id;
        moveToNextStatusCommand.DocumentTemplateFileType = command.DocumentTemplateFileType;
        moveToNextStatusCommand.File = command.File;
        moveToNextStatusCommand.UserId = command.UserId;
        moveToNextStatusCommand.OriginalDocumentTemplateSMTA1DocumentId = command.OriginalDocumentTemplateSMTA1DocumentId;
        moveToNextStatusCommand.IsSaveDraft = command.IsSaveDraft;

        return moveToNextStatusCommand;
    }

    private async Task<Errors?> CreateHistoryItem(SMTA1WorkflowItem SMTA1WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction transaction)
    {
        SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem = new SMTA1WorkflowHistoryItem();
        SMTA1WorkflowHistoryItem.Id = Guid.NewGuid();
        SMTA1WorkflowHistoryItem.SMTA1WorkflowItemId = SMTA1WorkflowItem.Id;
        SMTA1WorkflowHistoryItem.LaboratoryId = SMTA1WorkflowItem.LaboratoryId;
        SMTA1WorkflowHistoryItem.Status = SMTA1WorkflowItem.Status;
        SMTA1WorkflowHistoryItem.PreviousStatus = SMTA1WorkflowItem.PreviousStatus;
        SMTA1WorkflowHistoryItem.CreationDate = DateTime.UtcNow;
        SMTA1WorkflowHistoryItem.OperationDate = SMTA1WorkflowItem.OperationDate;
        SMTA1WorkflowHistoryItem.LastOperationUserId = SMTA1WorkflowItem.LastOperationUserId;
        SMTA1WorkflowHistoryItem.Comment = SMTA1WorkflowItem.Comment;
        SMTA1WorkflowHistoryItem.WorkflowItemTitle = SMTA1WorkflowItem.WorkflowItemTitle;
        SMTA1WorkflowHistoryItem.LastSubmissionApproved = SMTA1WorkflowItem.LastSubmissionApproved;

        SMTA1WorkflowHistoryItem.ReferenceId = SMTA1WorkflowItem.ReferenceId;
        SMTA1WorkflowHistoryItem.IsPast = SMTA1WorkflowItem.IsPast;



        var result = await _SMTA1WorkflowHistoryItemWriteRepository.Create(SMTA1WorkflowHistoryItem, cancellationToken, transaction);
        if (result.IsRight)
            throw new Exception(result.Right.ToString());

        await _SMTA1WorkflowHistoryItemWriteRepository.CopyLinkDocumentFromSMTA1WorkflowItem(SMTA1WorkflowItem.Id, SMTA1WorkflowHistoryItem.Id, cancellationToken, transaction);

        return null;
    }
}