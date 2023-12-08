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

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItem;

public interface IUpdateWorklistFromBioHubItemHandler
{
    Task<Either<UpdateWorklistFromBioHubItemCommandResponse, Errors>> Handle(UpdateWorklistFromBioHubItemCommand command, CancellationToken cancellationToken);
}

public class UpdateWorklistFromBioHubItemHandler : IUpdateWorklistFromBioHubItemHandler
{
    private readonly ILogger<UpdateWorklistFromBioHubItemHandler> _logger;
    private readonly UpdateWorklistFromBioHubItemCommandValidator _validator;
    private readonly IUpdateWorklistFromBioHubItemMapper _mapper;
    private readonly IWorklistFromBioHubItemWriteRepository _writeRepository;
    private readonly IWorklistFromBioHubEngine _worklistFromBioHubEngine;
    private readonly IWorklistFromBioHubHistoryItemWriteRepository _worklistFromBioHubHistoryItemWriteRepository;
    private readonly IWorklistFromBioHubEmailNotifier _worklistFromBioHubEmailNotifier;


    public UpdateWorklistFromBioHubItemHandler(
        ILogger<UpdateWorklistFromBioHubItemHandler> logger,
        UpdateWorklistFromBioHubItemCommandValidator validator,
        IUpdateWorklistFromBioHubItemMapper mapper,
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

    public async Task<Either<UpdateWorklistFromBioHubItemCommandResponse, Errors>> Handle(
        UpdateWorklistFromBioHubItemCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            // return new(new Errors(validationResult));
            return new(new Errors(ErrorType.RequestParsing, validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));

        IDbContextTransaction transaction = null;

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

            if (worklistfrombiohubitem.ReferenceId != command.ReferenceId)
            {
                return new(new Errors(ErrorType.NotFound, $"The current page is not up to date - Please refresh the page"));

            }

            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistfrombiohubitem.Status, PermissionType.Update, worklistfrombiohubitem.IsPast);

            if (!command.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Action forbidden"));
            }
            transaction = await _writeRepository.BeginTransactionAsync();

            if (command.IsSaveDraft != true)
            {
                await CreateHistoryItem(worklistfrombiohubitem, cancellationToken, transaction);
            }

            worklistfrombiohubitem = _mapper.Map(worklistfrombiohubitem, command);

            var moveToNextStatusCommand = PrepareMoveToNextStatusCommand(command);

            if (command.LastSubmissionApproved == true || command.IsSaveDraft == true)
            {
                worklistfrombiohubitem = await _worklistFromBioHubEngine.MoveToNextStatusUponApproveOrSaveDraft(worklistfrombiohubitem, moveToNextStatusCommand, cancellationToken, transaction);
            }
            else
            {
                worklistfrombiohubitem = await _worklistFromBioHubEngine.MoveToNextStatusUponReject(worklistfrombiohubitem, moveToNextStatusCommand, cancellationToken, transaction);
            }

            await transaction.CommitAsync();
            await transaction.DisposeAsync();

            if (command.IsSaveDraft != true && worklistfrombiohubitem.IsPast != true)
            {
                await _worklistFromBioHubEmailNotifier.NotifyUsers(worklistfrombiohubitem, cancellationToken);
            }

            return new(new UpdateWorklistFromBioHubItemCommandResponse(worklistfrombiohubitem.Id));

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

    private MoveToNextStatusFromBioHubEngineCommand PrepareMoveToNextStatusCommand(UpdateWorklistFromBioHubItemCommand command)
    {
        MoveToNextStatusFromBioHubEngineCommand moveToNextStatusCommand = new MoveToNextStatusFromBioHubEngineCommand();
        moveToNextStatusCommand.Id = command.Id;
        moveToNextStatusCommand.DocumentTemplateFileType = command.DocumentTemplateFileType;
        moveToNextStatusCommand.File = command.File;
        moveToNextStatusCommand.UserId = command.UserId;
        moveToNextStatusCommand.OriginalDocumentTemplateSMTA2DocumentId = command.OriginalDocumentTemplateSMTA2DocumentId;
        moveToNextStatusCommand.WorklistFromBioHubItemMaterials = command.WorklistFromBioHubItemMaterials;
        moveToNextStatusCommand.WorklistFromBioHubItemLaboratoryFocalPoints = command.LaboratoryFocalPoints;
        moveToNextStatusCommand.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId = command.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId;
        moveToNextStatusCommand.WorklistFromBioHubItemAnnex2OfSMTA2Conditions = command.WorklistFromBioHubItemAnnex2OfSMTA2Conditions;
        moveToNextStatusCommand.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s = command.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s;
        moveToNextStatusCommand.BookingForms = command.BookingForms;
        moveToNextStatusCommand.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId = command.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId;
        moveToNextStatusCommand.Annex2FillingOption = command.Annex2FillingOption;
        moveToNextStatusCommand.BookingFormFillingOption = command.BookingFormFillingOption;
        moveToNextStatusCommand.BiosafetyChecklistFillingOption = command.BiosafetyChecklistFillingOption;
        moveToNextStatusCommand.IsSaveDraft = command.IsSaveDraft;
        moveToNextStatusCommand.ShipmentDocumentId = command.ShipmentDocumentId;
        moveToNextStatusCommand.ShipmentDocumentNewName = command.ShipmentDocumentNewName;
        moveToNextStatusCommand.ShipmentDocumentOperationType = command.ShipmentDocumentOperationType;
        moveToNextStatusCommand.BHFShipmentDocuments = command.BHFShipmentDocuments;
        moveToNextStatusCommand.QEShipmentDocuments = command.QEShipmentDocuments;
        moveToNextStatusCommand.Feedbacks = command.Feedbacks;
        moveToNextStatusCommand.NewFeedback = command.NewFeedback;

        moveToNextStatusCommand.BiosafetyChecklistThreadComments = command.BiosafetyChecklistThreadComments;
        moveToNextStatusCommand.NewBiosafetyChecklistThreadComment = command.NewBiosafetyChecklistThreadComment;

        moveToNextStatusCommand.NewBiosafetyChecklistThreadCommentFromDocument = command.NewBiosafetyChecklistThreadCommentFromDocument;
        moveToNextStatusCommand.PreviousOperationDate = command.PreviousOperationDate;
        moveToNextStatusCommand.PreviousUserId = command.PreviousUserId;
        moveToNextStatusCommand.CurrentDownloadSMTA2DocumentId = command.CurrentDownloadSMTA2DocumentId;

        return moveToNextStatusCommand;
    }

    private async Task<Errors?> CreateHistoryItem(WorklistFromBioHubItem worklistFromBioHubItem, CancellationToken cancellationToken, IDbContextTransaction transaction)
    {
        WorklistFromBioHubHistoryItem worklistFromBioHubHistoryItem = new WorklistFromBioHubHistoryItem();
        worklistFromBioHubHistoryItem.Id = Guid.NewGuid();
        worklistFromBioHubHistoryItem.WorklistFromBioHubItemId = worklistFromBioHubItem.Id;
        worklistFromBioHubHistoryItem.RequestInitiationFromBioHubFacilityId = worklistFromBioHubItem.RequestInitiationFromBioHubFacilityId;
        worklistFromBioHubHistoryItem.RequestInitiationToLaboratoryId = worklistFromBioHubItem.RequestInitiationToLaboratoryId;
        worklistFromBioHubHistoryItem.Status = worklistFromBioHubItem.Status;
        worklistFromBioHubHistoryItem.PreviousStatus = worklistFromBioHubItem.PreviousStatus;
        worklistFromBioHubHistoryItem.CreationDate = DateTime.UtcNow;
        worklistFromBioHubHistoryItem.OperationDate = worklistFromBioHubItem.OperationDate;
        worklistFromBioHubHistoryItem.LastOperationUserId = worklistFromBioHubItem.LastOperationUserId;
        worklistFromBioHubHistoryItem.Comment = worklistFromBioHubItem.Comment;
        worklistFromBioHubHistoryItem.WorklistItemTitle = worklistFromBioHubItem.WorklistItemTitle;
        worklistFromBioHubHistoryItem.LastSubmissionApproved = worklistFromBioHubItem.LastSubmissionApproved;
        worklistFromBioHubHistoryItem.Annex2Comment = worklistFromBioHubItem.Annex2Comment;
        worklistFromBioHubHistoryItem.Annex2FillingOption = worklistFromBioHubItem.Annex2FillingOption;
        worklistFromBioHubHistoryItem.Annex2TermsAndConditions = worklistFromBioHubItem.Annex2TermsAndConditions;
        worklistFromBioHubHistoryItem.Annex2ApprovalFlag = worklistFromBioHubItem.Annex2ApprovalFlag;
        worklistFromBioHubHistoryItem.Annex2ApprovalComment = worklistFromBioHubItem.Annex2ApprovalComment;
        worklistFromBioHubHistoryItem.BiosafetyChecklistFillingOption = worklistFromBioHubItem.BiosafetyChecklistFillingOption;
        worklistFromBioHubHistoryItem.BiosafetyChecklistApprovalFlag = worklistFromBioHubItem.BiosafetyChecklistApprovalFlag;
        worklistFromBioHubHistoryItem.BiosafetyChecklistApprovalComment = worklistFromBioHubItem.BiosafetyChecklistApprovalComment;
        worklistFromBioHubHistoryItem.BookingFormFillingOption = worklistFromBioHubItem.BookingFormFillingOption;
        worklistFromBioHubHistoryItem.BookingFormApprovalFlag = worklistFromBioHubItem.BookingFormApprovalFlag;
        worklistFromBioHubHistoryItem.BookingFormApprovalComment = worklistFromBioHubItem.BookingFormApprovalComment;
        worklistFromBioHubHistoryItem.WaitForArrivalConditionCheckApprovalComment = worklistFromBioHubItem.WaitForArrivalConditionCheckApprovalComment;
        worklistFromBioHubHistoryItem.WaitForArrivalConditionCheckApprovalFlag = worklistFromBioHubItem.WaitForArrivalConditionCheckApprovalFlag;
        worklistFromBioHubHistoryItem.ReferenceId = worklistFromBioHubItem.ReferenceId;
        worklistFromBioHubHistoryItem.IsPast = worklistFromBioHubItem.IsPast;

        worklistFromBioHubHistoryItem.Annex2OfSMTA2ApprovalDate = worklistFromBioHubItem.Annex2OfSMTA2ApprovalDate;
        worklistFromBioHubHistoryItem.BookingFormOfSMTA2ApprovalDate = worklistFromBioHubItem.BookingFormOfSMTA2ApprovalDate;
        worklistFromBioHubHistoryItem.BiosafetyChecklistApprovalDate = worklistFromBioHubItem.BiosafetyChecklistApprovalDate;

        //# 54317
        worklistFromBioHubHistoryItem.Annex2OfSMTA2SignatureText = worklistFromBioHubItem.Annex2OfSMTA2SignatureText;
        worklistFromBioHubHistoryItem.BiosafetyChecklistOfSMTA2SignatureText = worklistFromBioHubItem.BiosafetyChecklistOfSMTA2SignatureText;
        worklistFromBioHubHistoryItem.BookingFormOfSMTA2SignatureText = worklistFromBioHubItem.BookingFormOfSMTA2SignatureText;
        ///////////////
        ///

        var result = await _worklistFromBioHubHistoryItemWriteRepository.Create(worklistFromBioHubHistoryItem, cancellationToken, transaction);
        if (result.IsRight)
            throw new Exception(result.Right.ToString());

        await _worklistFromBioHubHistoryItemWriteRepository.CopyLinkDocumentFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistFromBioHubHistoryItemWriteRepository.LinkMaterialsFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistFromBioHubHistoryItemWriteRepository.LinkLaboratoryFocalPointsFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistFromBioHubHistoryItemWriteRepository.LinkAnnex2OfSMTA2ConditionsFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistFromBioHubHistoryItemWriteRepository.LinkBiosafetyChecklistOfSMTA2sFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistFromBioHubHistoryItemWriteRepository.LinkBookingFormsFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistFromBioHubHistoryItemWriteRepository.LinkFeedbacksFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        await _worklistFromBioHubHistoryItemWriteRepository.LinkBiosafetyChecklistCommentsFromWorklistFromBioHubItem(worklistFromBioHubItem.Id, worklistFromBioHubHistoryItem.Id, cancellationToken, transaction);
        return null;
    }
}