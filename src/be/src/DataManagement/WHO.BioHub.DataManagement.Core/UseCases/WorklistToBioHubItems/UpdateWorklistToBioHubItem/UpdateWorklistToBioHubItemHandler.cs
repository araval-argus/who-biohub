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

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;

public interface IUpdateWorklistToBioHubItemHandler
{
    Task<Either<UpdateWorklistToBioHubItemCommandResponse, Errors>> Handle(UpdateWorklistToBioHubItemCommand command, CancellationToken cancellationToken);
}

public class UpdateWorklistToBioHubItemHandler : IUpdateWorklistToBioHubItemHandler
{
    private readonly ILogger<UpdateWorklistToBioHubItemHandler> _logger;
    private readonly UpdateWorklistToBioHubItemCommandValidator _validator;
    private readonly IUpdateWorklistToBioHubItemMapper _mapper;
    private readonly IWorklistToBioHubItemWriteRepository _writeRepository;
    private readonly IWorklistToBioHubEngine _worklistToBioHubEngine;
    private readonly IWorklistToBioHubHistoryItemWriteRepository _worklistToBioHubHistoryItemWriteRepository;
    private readonly IWorklistToBioHubEmailNotifier _worklistToBioHubEmailNotifier;


    public UpdateWorklistToBioHubItemHandler(
        ILogger<UpdateWorklistToBioHubItemHandler> logger,
        UpdateWorklistToBioHubItemCommandValidator validator,
        IUpdateWorklistToBioHubItemMapper mapper,
        IWorklistToBioHubItemWriteRepository writeRepository,
        IWorklistToBioHubEngine worklistToBioHubEngine,
        IWorklistToBioHubHistoryItemWriteRepository worklistToBioHubHistoryItemWriteRepository,
        IWorklistToBioHubEmailNotifier worklistToBioHubEmailNotifier
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
        _worklistToBioHubEngine = worklistToBioHubEngine;
        _worklistToBioHubHistoryItemWriteRepository = worklistToBioHubHistoryItemWriteRepository;
        _worklistToBioHubEmailNotifier = worklistToBioHubEmailNotifier;
    }

    public async Task<Either<UpdateWorklistToBioHubItemCommandResponse, Errors>> Handle(
        UpdateWorklistToBioHubItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            // return new(new Errors(validationResult));
            return new(new Errors(ErrorType.RequestParsing, validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));

        IDbContextTransaction transaction = null;

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

            if (worklisttobiohubitem.Status != command.CurrentStatus)
            {
                return new(new Errors(ErrorType.NotFound, $"Item Status mismatch with the current page status - Please refresh the page"));
            }

            if (worklisttobiohubitem.ReferenceId != command.ReferenceId)
            {
                return new(new Errors(ErrorType.NotFound, $"The current page is not up to date - Please refresh the page"));

            }

            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklisttobiohubitem.Status, PermissionType.Update, worklisttobiohubitem.IsPast);

            if (!command.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Action forbidden"));
            }

            transaction = await _writeRepository.BeginTransactionAsync();

            if (command.IsSaveDraft != true)
            {
                await CreateHistoryItem(worklisttobiohubitem, cancellationToken, transaction);
            }

            worklisttobiohubitem = _mapper.Map(worklisttobiohubitem, command);

            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);

            if (command.LastSubmissionApproved == true || command.IsSaveDraft == true)
            {
                worklisttobiohubitem = await _worklistToBioHubEngine.MoveToNextStatusUponApproveOrSaveDraft(worklisttobiohubitem, moveToNextStatusCommand, cancellationToken, transaction);
            }
            else
            {
                worklisttobiohubitem = await _worklistToBioHubEngine.MoveToNextStatusUponReject(worklisttobiohubitem, moveToNextStatusCommand, cancellationToken, transaction);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            if (command.IsSaveDraft != true && worklisttobiohubitem.IsPast != true)
            {
                await _worklistToBioHubEmailNotifier.NotifyUsers(worklisttobiohubitem, cancellationToken);
            }

            return new(new UpdateWorklistToBioHubItemCommandResponse(worklisttobiohubitem.Id));

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

    private MoveToNextStatusToBioHubEngineCommand PrepareMoveToNextStatusCommand(UpdateWorklistToBioHubItemCommand command)
    {
        MoveToNextStatusToBioHubEngineCommand moveToNextStatusCommand = new MoveToNextStatusToBioHubEngineCommand();
        moveToNextStatusCommand.Id = command.Id;
        moveToNextStatusCommand.DocumentTemplateFileType = command.DocumentTemplateFileType;
        moveToNextStatusCommand.File = command.File;
        moveToNextStatusCommand.UserId = command.UserId;
        moveToNextStatusCommand.OriginalDocumentTemplateSMTA1DocumentId = command.OriginalDocumentTemplateSMTA1DocumentId;
        moveToNextStatusCommand.MaterialShippingInformations = command.MaterialShippingInformations;
        moveToNextStatusCommand.WorklistToBioHubItemLaboratoryFocalPoints = command.LaboratoryFocalPoints;
        moveToNextStatusCommand.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId = command.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId;
        moveToNextStatusCommand.BookingForms = command.BookingForms;
        moveToNextStatusCommand.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId = command.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId;
        moveToNextStatusCommand.Annex2FillingOption = command.Annex2FillingOption;
        moveToNextStatusCommand.BookingFormFillingOption = command.BookingFormFillingOption;
        moveToNextStatusCommand.IsSaveDraft = command.IsSaveDraft;
        moveToNextStatusCommand.ShipmentDocumentId = command.ShipmentDocumentId;
        moveToNextStatusCommand.ShipmentDocumentNewName = command.ShipmentDocumentNewName;
        moveToNextStatusCommand.ShipmentDocumentOperationType = command.ShipmentDocumentOperationType;
        moveToNextStatusCommand.ShipmentDocuments = command.ShipmentDocuments;
        moveToNextStatusCommand.feedbacks = command.feedbacks;
        moveToNextStatusCommand.NewFeedback = command.NewFeedback;
        moveToNextStatusCommand.WorklistToBioHubItemMaterials = command.WorklistToBioHubItemMaterials;
        moveToNextStatusCommand.WorklistToBioHubItemBioHubFacilityFocalPoints = command.WorklistToBioHubItemBioHubFacilityFocalPoints;
        moveToNextStatusCommand.CurrentDownloadSMTA1DocumentId = command.CurrentDownloadSMTA1DocumentId;

        return moveToNextStatusCommand;
    }

    private async Task<Errors?> CreateHistoryItem(WorklistToBioHubItem worklistToBioHubItem, CancellationToken cancellationToken, IDbContextTransaction transaction)
    {
        WorklistToBioHubHistoryItem worklistToBioHubHistoryItem = new WorklistToBioHubHistoryItem();
        worklistToBioHubHistoryItem.Id = Guid.NewGuid();
        worklistToBioHubHistoryItem.WorklistToBioHubItemId = worklistToBioHubItem.Id;
        worklistToBioHubHistoryItem.RequestInitiationToBioHubFacilityId = worklistToBioHubItem.RequestInitiationToBioHubFacilityId;
        worklistToBioHubHistoryItem.RequestInitiationFromLaboratoryId = worklistToBioHubItem.RequestInitiationFromLaboratoryId;
        worklistToBioHubHistoryItem.Status = worklistToBioHubItem.Status;
        worklistToBioHubHistoryItem.PreviousStatus = worklistToBioHubItem.PreviousStatus;
        worklistToBioHubHistoryItem.CreationDate = DateTime.UtcNow;
        worklistToBioHubHistoryItem.OperationDate = worklistToBioHubItem.OperationDate;
        worklistToBioHubHistoryItem.LastOperationUserId = worklistToBioHubItem.LastOperationUserId;
        worklistToBioHubHistoryItem.Comment = worklistToBioHubItem.Comment;
        worklistToBioHubHistoryItem.WorklistItemTitle = worklistToBioHubItem.WorklistItemTitle;
        worklistToBioHubHistoryItem.LastSubmissionApproved = worklistToBioHubItem.LastSubmissionApproved;
        worklistToBioHubHistoryItem.Annex2Comment = worklistToBioHubItem.Annex2Comment;
        worklistToBioHubHistoryItem.Annex2FillingOption = worklistToBioHubItem.Annex2FillingOption;
        worklistToBioHubHistoryItem.Annex2TermsAndConditions = worklistToBioHubItem.Annex2TermsAndConditions;
        worklistToBioHubHistoryItem.Annex2ApprovalFlag = worklistToBioHubItem.Annex2ApprovalFlag;
        worklistToBioHubHistoryItem.Annex2ApprovalComment = worklistToBioHubItem.Annex2ApprovalComment;

        worklistToBioHubHistoryItem.BookingFormFillingOption = worklistToBioHubItem.BookingFormFillingOption;

        worklistToBioHubHistoryItem.BookingFormApprovalFlag = worklistToBioHubItem.BookingFormApprovalFlag;
        worklistToBioHubHistoryItem.BookingFormApprovalComment = worklistToBioHubItem.BookingFormApprovalComment;
        worklistToBioHubHistoryItem.WaitForArrivalConditionCheckApprovalComment = worklistToBioHubItem.WaitForArrivalConditionCheckApprovalComment;
        worklistToBioHubHistoryItem.WaitForArrivalConditionCheckApprovalFlag = worklistToBioHubItem.WaitForArrivalConditionCheckApprovalFlag;
        worklistToBioHubHistoryItem.ReferenceId = worklistToBioHubItem.ReferenceId;
        worklistToBioHubHistoryItem.IsPast = worklistToBioHubItem.IsPast;
        worklistToBioHubHistoryItem.OriginalBookingFormOfSMTA1DocumentTemplateId = worklistToBioHubItem.OriginalBookingFormOfSMTA1DocumentTemplateId;
        worklistToBioHubHistoryItem.OriginalAnnex2OfSMTA1DocumentTemplateId = worklistToBioHubItem.OriginalAnnex2OfSMTA1DocumentTemplateId;

        worklistToBioHubHistoryItem.Annex2OfSMTA1ApprovalDate = worklistToBioHubItem.Annex2OfSMTA1ApprovalDate;
        worklistToBioHubHistoryItem.BookingFormOfSMTA1ApprovalDate = worklistToBioHubItem.BookingFormOfSMTA1ApprovalDate;

        //# 54317
        worklistToBioHubHistoryItem.Annex2OfSMTA1SignatureText = worklistToBioHubItem.Annex2OfSMTA1SignatureText;
        worklistToBioHubHistoryItem.BookingFormOfSMTA1SignatureText = worklistToBioHubItem.BookingFormOfSMTA1SignatureText;
        /////////////////

        var result = await _worklistToBioHubHistoryItemWriteRepository.Create(worklistToBioHubHistoryItem, cancellationToken, transaction);
        if (result.IsRight)
            throw new Exception(result.Right.ToString());

        await _worklistToBioHubHistoryItemWriteRepository.CopyLinkDocumentFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistToBioHubHistoryItemWriteRepository.LinkMaterialShippingInformationFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistToBioHubHistoryItemWriteRepository.LinkLaboratoryFocalPointsFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistToBioHubHistoryItemWriteRepository.LinkBookingFormsFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistToBioHubHistoryItemWriteRepository.LinkFeedbacksFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistToBioHubHistoryItemWriteRepository.LinkBioHubFacilityFocalPointsFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubHistoryItem.Id, cancellationToken, transaction);

        return null;
    }
}