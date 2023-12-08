using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;

public interface IUpdateSMTA2WorkflowItemHandler
{
    Task<Either<UpdateSMTA2WorkflowItemCommandResponse, Errors>> Handle(UpdateSMTA2WorkflowItemCommand command, CancellationToken cancellationToken);
}

public class UpdateSMTA2WorkflowItemHandler : IUpdateSMTA2WorkflowItemHandler
{
    private readonly ILogger<UpdateSMTA2WorkflowItemHandler> _logger;
    private readonly UpdateSMTA2WorkflowItemCommandValidator _validator;
    private readonly IUpdateSMTA2WorkflowItemMapper _mapper;
    private readonly ISMTA2WorkflowItemWriteRepository _writeRepository;
    private readonly ISMTA2WorkflowEngine _SMTA2WorkflowEngine;
    private readonly ISMTA2WorkflowHistoryItemWriteRepository _SMTA2WorkflowHistoryItemWriteRepository;
    private readonly ISMTA2WorkflowEmailNotifier _SMTA2WorkflowEmailNotifier;


    public UpdateSMTA2WorkflowItemHandler(
        ILogger<UpdateSMTA2WorkflowItemHandler> logger,
        UpdateSMTA2WorkflowItemCommandValidator validator,
        IUpdateSMTA2WorkflowItemMapper mapper,
        ISMTA2WorkflowItemWriteRepository writeRepository,
        ISMTA2WorkflowEngine SMTA2WorkflowEngine,
        ISMTA2WorkflowHistoryItemWriteRepository SMTA2WorkflowHistoryItemWriteRepository,
        ISMTA2WorkflowEmailNotifier SMTA2WorkflowEmailNotifier
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _SMTA2WorkflowEngine = SMTA2WorkflowEngine;
        _SMTA2WorkflowHistoryItemWriteRepository = SMTA2WorkflowHistoryItemWriteRepository;
        _SMTA2WorkflowEmailNotifier = SMTA2WorkflowEmailNotifier;
    }

    public async Task<Either<UpdateSMTA2WorkflowItemCommandResponse, Errors>> Handle(
        UpdateSMTA2WorkflowItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            // return new(new Errors(validationResult));
            return new(new Errors(ErrorType.RequestParsing, validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));

        IDbContextTransaction transaction = null;

        try
        {
            SMTA2WorkflowItem SMTA2WorkflowItem = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);

            if (SMTA2WorkflowItem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
            }

            switch (command.RoleType)
            {
                case RoleType.Laboratory:
                    if (SMTA2WorkflowItem.LaboratoryId != command.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {command.Id} not found"));
                    }
                    break;

            }

            if (SMTA2WorkflowItem.Status != command.CurrentStatus)
            {
                return new(new Errors(ErrorType.NotFound, $"Item Status mismatch with the current page status - Please refresh the page"));
            }

            if (SMTA2WorkflowItem.ReferenceId != command.ReferenceId)
            {
                return new(new Errors(ErrorType.NotFound, $"The current page is not up to date - Please refresh the page"));

            }

            var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(SMTA2WorkflowItem.Status, PermissionType.Update, SMTA2WorkflowItem.IsPast);

            if (!command.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Action forbidden"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            if (command.IsSaveDraft != true)
            {
                await CreateHistoryItem(SMTA2WorkflowItem, cancellationToken, transaction);
            }

            SMTA2WorkflowItem = _mapper.Map(SMTA2WorkflowItem, command);

            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);

            if (command.LastSubmissionApproved == true || command.IsSaveDraft == true)
            {
                SMTA2WorkflowItem = await _SMTA2WorkflowEngine.MoveToNextStatusUponApproveOrSaveDraft(SMTA2WorkflowItem, moveToNextStatusCommand, cancellationToken, transaction);
            }
            else
            {
                SMTA2WorkflowItem = await _SMTA2WorkflowEngine.MoveToNextStatusUponReject(SMTA2WorkflowItem, moveToNextStatusCommand, cancellationToken, transaction);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            if (command.IsSaveDraft != true && SMTA2WorkflowItem.IsPast != true)
            {
                await _SMTA2WorkflowEmailNotifier.NotifyUsers(SMTA2WorkflowItem, cancellationToken);
            }

            return new(new UpdateSMTA2WorkflowItemCommandResponse(SMTA2WorkflowItem.Id));

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

    private MoveToNextStatusSMTA2WorkflowEngineCommand PrepareMoveToNextStatusCommand(UpdateSMTA2WorkflowItemCommand command)
    {
        MoveToNextStatusSMTA2WorkflowEngineCommand moveToNextStatusCommand = new MoveToNextStatusSMTA2WorkflowEngineCommand();
        moveToNextStatusCommand.Id = command.Id;
        moveToNextStatusCommand.DocumentTemplateFileType = command.DocumentTemplateFileType;
        moveToNextStatusCommand.File = command.File;
        moveToNextStatusCommand.UserId = command.UserId;
        moveToNextStatusCommand.OriginalDocumentTemplateSMTA2DocumentId = command.OriginalDocumentTemplateSMTA2DocumentId;
        moveToNextStatusCommand.IsSaveDraft = command.IsSaveDraft;

        return moveToNextStatusCommand;
    }

    private async Task<Errors?> CreateHistoryItem(SMTA2WorkflowItem SMTA2WorkflowItem, CancellationToken cancellationToken, IDbContextTransaction transaction)
    {
        SMTA2WorkflowHistoryItem SMTA2WorkflowHistoryItem = new SMTA2WorkflowHistoryItem();
        SMTA2WorkflowHistoryItem.Id = Guid.NewGuid();
        SMTA2WorkflowHistoryItem.SMTA2WorkflowItemId = SMTA2WorkflowItem.Id;
        SMTA2WorkflowHistoryItem.LaboratoryId = SMTA2WorkflowItem.LaboratoryId;
        SMTA2WorkflowHistoryItem.Status = SMTA2WorkflowItem.Status;
        SMTA2WorkflowHistoryItem.PreviousStatus = SMTA2WorkflowItem.PreviousStatus;
        SMTA2WorkflowHistoryItem.CreationDate = DateTime.UtcNow;
        SMTA2WorkflowHistoryItem.OperationDate = SMTA2WorkflowItem.OperationDate;
        SMTA2WorkflowHistoryItem.LastOperationUserId = SMTA2WorkflowItem.LastOperationUserId;
        SMTA2WorkflowHistoryItem.Comment = SMTA2WorkflowItem.Comment;
        SMTA2WorkflowHistoryItem.WorkflowItemTitle = SMTA2WorkflowItem.WorkflowItemTitle;
        SMTA2WorkflowHistoryItem.LastSubmissionApproved = SMTA2WorkflowItem.LastSubmissionApproved;

        SMTA2WorkflowHistoryItem.ReferenceId = SMTA2WorkflowItem.ReferenceId;
        SMTA2WorkflowHistoryItem.IsPast = SMTA2WorkflowItem.IsPast;



        var result = await _SMTA2WorkflowHistoryItemWriteRepository.Create(SMTA2WorkflowHistoryItem, cancellationToken, transaction);
        if (result.IsRight)
            throw new Exception(result.Right.ToString());

        await _SMTA2WorkflowHistoryItemWriteRepository.CopyLinkDocumentFromSMTA2WorkflowItem(SMTA2WorkflowItem.Id, SMTA2WorkflowHistoryItem.Id, cancellationToken, transaction);

        return null;
    }
}