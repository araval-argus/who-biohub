using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubEmails;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Notifications;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public class WorklistFromBioHubEngine : IWorklistFromBioHubEngine
    {
        private readonly IDocumentReadRepository _documentReadRepository;
        private readonly IWorklistFromBioHubItemWriteRepository _worklistFromBioHubItemWriteRepository;
        private readonly IStorageAccountUtility _storageAccountUtility;
        private readonly IDocumentWriteRepository _writeDocumentRepository;
        private readonly IShipmentWriteRepository _shipmentWriteRepository;
        private readonly IWorklistItemUsedReferenceNumberWriteRepository _writeUsedReferenceNumberRepository;

        public WorklistFromBioHubEngine(            IDocumentReadRepository documentReadRepository,            IWorklistFromBioHubItemWriteRepository worklistFromBioHubItemWriteRepository,            IStorageAccountUtility storageAccountUtility,            IDocumentWriteRepository writeDocumentRepository,            IShipmentWriteRepository shipmentWriteRepository,            IWorklistItemUsedReferenceNumberWriteRepository writeUsedReferenceNumberRepository
        )
        {
            _documentReadRepository = documentReadRepository;
            _worklistFromBioHubItemWriteRepository = worklistFromBioHubItemWriteRepository;
            _storageAccountUtility = storageAccountUtility;
            _writeDocumentRepository = writeDocumentRepository;
            _shipmentWriteRepository = shipmentWriteRepository;
            _writeUsedReferenceNumberRepository = writeUsedReferenceNumberRepository;
        }


        public async Task<WorklistFromBioHubItem> MoveToNextStatusUponApproveOrSaveDraft(
            WorklistFromBioHubItem worklistFromBioHubItem,
            MoveToNextStatusFromBioHubEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null
        )
        {
            WorklistFromBioHubStatus currentStatus = worklistFromBioHubItem.Status;
            Errors? errors;
            bool isDocumentFile;
            Shipment shipment;
            Either<Shipment, Errors> shipmentCreationResult;
            DocumentFileType documentType;
            Guid laboratoryId;

            switch (currentStatus)
            {
                case WorklistFromBioHubStatus.RequestInitiation:
                    laboratoryId = worklistFromBioHubItem.RequestInitiationToLaboratoryId.GetValueOrDefault();
                    documentType = DocumentFileType.SMTA2;

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2;                

                    var worklistItemUsedReferenceNumber = await _writeUsedReferenceNumberRepository.Create(worklistFromBioHubItem.IsPast, cancellationToken, transaction);
                    if (worklistItemUsedReferenceNumber.IsRight)
                        throw new Exception(worklistItemUsedReferenceNumber.Right.ToString());

                    worklistFromBioHubItem.ReferenceNumber = worklistItemUsedReferenceNumber.Left.ReferenceNumber;

                    var result = await _worklistFromBioHubItemWriteRepository.Create(worklistFromBioHubItem, cancellationToken, transaction);
                    if (result.IsRight)
                        throw new Exception(result.Right.ToString());

                    break;



                case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:
                    if (command.IsSaveDraft != true)
                    {
                        worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval;
                    }
                    isDocumentFile = worklistFromBioHubItem.Annex2FillingOption == FillingOption.DocumentUpload;

                    if (command.File != null)
                    {
                        await SetNewDocument(worklistFromBioHubItem, command, command.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId, false, isDocumentFile, cancellationToken, transaction);
                    }
                    if (worklistFromBioHubItem.Annex2FillingOption == FillingOption.ElectronicallyFill)
                    {
                        await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);
                        await SetNewWorklistFromBioHubItemLaboratoryFocalPoint(worklistFromBioHubItem, command, cancellationToken, transaction);
                        await SetNewAnnexOfSMTA2Conditions(worklistFromBioHubItem, command, true, cancellationToken, transaction);
                    }
                    else
                    {
                        var linkMaterialsResult = await _worklistFromBioHubItemWriteRepository.LinkMaterials(worklistFromBioHubItem.Id, null, cancellationToken, transaction);

                        if (linkMaterialsResult != null)
                        {
                            throw new Exception("Error Linking new Materials to worklistFromBioHubItem");
                        }
                    }

                    if (worklistFromBioHubItem.IsPast == true)
                    {
                        errors = await _worklistFromBioHubItemWriteRepository.LinkDocument(worklistFromBioHubItem.Id, command.CurrentDownloadSMTA2DocumentId.GetValueOrDefault(), DocumentFileType.SMTA2, cancellationToken, true, transaction, true);
                        if (errors.HasValue)
                            throw new Exception(errors.Value.ToString());
                    }

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;

                case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2;
                    worklistFromBioHubItem.Annex2OfSMTA2ApprovalDate = worklistFromBioHubItem.OperationDate;

                    command = PrepareBookingForms(worklistFromBioHubItem, command);

                    await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);
                    await SetNewWorklistFromBioHubItemLaboratoryFocalPoint(worklistFromBioHubItem, command, cancellationToken, transaction);
                    await SetNewAnnexOfSMTA2Conditions(worklistFromBioHubItem, command, true, cancellationToken, transaction);


                    await _worklistFromBioHubItemWriteRepository.LinkBookingForm(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    if (worklistFromBioHubItem.Annex2FillingOption == FillingOption.DocumentUpload)
                    {
                        var approveAnnex2Document = await _writeDocumentRepository.ApproveWorklistFromBioHubItemDocument(worklistFromBioHubItem.Id, DocumentFileType.Annex2OfSMTA2, cancellationToken, transaction);

                        if (approveAnnex2Document != null)
                        {
                            throw new Exception("Error approving Annex 2 of SMTA 2 document");
                        }
                    }

                    var linkDocumentResult = await _worklistFromBioHubItemWriteRepository.LinkDocument(worklistFromBioHubItem.Id, command.CurrentDownloadSMTA2DocumentId.GetValueOrDefault(), DocumentFileType.SMTA2, cancellationToken, true, transaction, true);

                    if (linkDocumentResult != null)
                    {
                        throw new Exception("Error Linking new document to worklistToBioHubItem");
                    }


                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;


                case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:
                    if (command.IsSaveDraft != true)
                    {
                        worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval;
                    }

                    isDocumentFile = worklistFromBioHubItem.BiosafetyChecklistFillingOption == FillingOption.DocumentUpload;

                    if (command.File != null)
                    {
                        await SetNewDocument(worklistFromBioHubItem, command, command.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId, false, isDocumentFile, cancellationToken, transaction);
                    }
                    if (worklistFromBioHubItem.BiosafetyChecklistFillingOption == FillingOption.ElectronicallyFill)
                    {
                        await SetNewBiosafetyChecklistOfSMTA2(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                        if (command.IsSaveDraft != true)
                        {
                            worklistFromBioHubItem.SavedBiosafetyChecklistThreadComment = null;
                            errors = await AddBiosafetyChecklistComment(worklistFromBioHubItem, command, transaction, cancellationToken);
                            if (errors.HasValue)
                                throw new Exception(errors.Value.ToString());
                        }
                        else
                        {
                            worklistFromBioHubItem.SavedBiosafetyChecklistThreadComment = command.NewBiosafetyChecklistThreadComment;
                        }
                    }

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;



                case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2;
                    worklistFromBioHubItem.BiosafetyChecklistApprovalDate = worklistFromBioHubItem.OperationDate;
                    
                    await SetNewBiosafetyChecklistOfSMTA2(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    errors = await AddBiosafetyChecklistComment(worklistFromBioHubItem, command, transaction, cancellationToken);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    if (worklistFromBioHubItem.BiosafetyChecklistFillingOption == FillingOption.DocumentUpload)
                    {
                        var approveBiosafetyChecklistDocument = await _writeDocumentRepository.ApproveWorklistFromBioHubItemDocument(worklistFromBioHubItem.Id, DocumentFileType.BiosafetyChecklist, cancellationToken, transaction);

                        if (approveBiosafetyChecklistDocument != null)
                        {
                            throw new Exception("Error approving BiosafetyChecklist document");
                        }
                    }

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;

                case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:
                    if (command.IsSaveDraft != true)
                    {
                        worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval;
                    }

                    isDocumentFile = worklistFromBioHubItem.BookingFormFillingOption == FillingOption.DocumentUpload;

                    if (command.File != null)
                    {
                        await SetNewDocument(worklistFromBioHubItem, command, command.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId, false, isDocumentFile, cancellationToken, transaction, true, true);
                    }
                    if (worklistFromBioHubItem.BookingFormFillingOption == FillingOption.ElectronicallyFill)
                    {
                        await _worklistFromBioHubItemWriteRepository.LinkBookingForm(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);
                        await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    }

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;

                case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForPickUpCompleted;
                    worklistFromBioHubItem.BookingFormOfSMTA2ApprovalDate = worklistFromBioHubItem.OperationDate;

                    await _worklistFromBioHubItemWriteRepository.LinkBookingForm(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    if (worklistFromBioHubItem.BookingFormFillingOption == FillingOption.DocumentUpload)
                    {
                        var approveBookingFormDocument = await _writeDocumentRepository.ApproveWorklistFromBioHubItemDocument(worklistFromBioHubItem.Id, DocumentFileType.BookingFormOfSMTA2, cancellationToken, transaction);

                        if (approveBookingFormDocument != null)
                        {
                            throw new Exception("Error approving Booking Form of SMTA 2 document");
                        }
                    }

                    if (worklistFromBioHubItem.BookingFormFillingOption == FillingOption.DocumentUpload)
                    {
                        await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);
                    }
                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;
              

                case WorklistFromBioHubStatus.WaitForPickUpCompleted:

                    if (command.IsSaveDraft != true)
                    {
                        worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForDeliveryCompleted;
                        if (worklistFromBioHubItem.IsPast != true)
                        {
                            errors = await _worklistFromBioHubItemWriteRepository.UpdateMaterialsCurrentNumberOfVials(worklistFromBioHubItem.Id, cancellationToken, transaction);
                            if (errors.HasValue)
                                throw new Exception(errors.Value.ToString());
                        }
                    }
                    await _worklistFromBioHubItemWriteRepository.UpdateBookingFormDeliveryProperties(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;


                case WorklistFromBioHubStatus.WaitForDeliveryCompleted:

                    if (command.IsSaveDraft != true)
                    {
                        worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForArrivalConditionCheck;
                    }

                    await _worklistFromBioHubItemWriteRepository.UpdateBookingFormDeliveryProperties(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;

                case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:

                    if (command.IsSaveDraft != true)
                    {
                        worklistFromBioHubItem.Status = WorklistFromBioHubStatus.ShipmentCompleted;
                    }

                    await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    if (command.IsSaveDraft != true)
                    {
                        shipment = CreateNewShipment(worklistFromBioHubItem);
                        shipmentCreationResult = await _shipmentWriteRepository.Create(shipment, cancellationToken, transaction);

                        if (shipmentCreationResult.IsRight)
                        {
                            throw new Exception(shipmentCreationResult.Right.ToString());
                        }
                    }
                    break;

                case WorklistFromBioHubStatus.WaitForCommentQESendFeedback:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForFinalApproval;

                    errors = await AddFeedback(worklistFromBioHubItem, command, transaction, cancellationToken);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;

                case WorklistFromBioHubStatus.WaitForFinalApproval:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.ShipmentCompleted;

                    await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    shipment = CreateNewShipment(worklistFromBioHubItem);
                    shipmentCreationResult = await _shipmentWriteRepository.Create(shipment, cancellationToken, transaction);

                    if (shipmentCreationResult.IsRight)
                    {
                        throw new Exception(shipmentCreationResult.Right.ToString());
                    }

                    break;


                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return worklistFromBioHubItem;
        }

        

        public async Task<WorklistFromBioHubItem> MoveToNextStatusUponReject(WorklistFromBioHubItem worklistFromBioHubItem,
            MoveToNextStatusFromBioHubEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            WorklistFromBioHubStatus currentStatus = worklistFromBioHubItem.Status;
            Errors? errors;

            switch (currentStatus)
            {

                case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2;

                    await CancelWorklistItemFile(worklistFromBioHubItem.Id, DocumentFileType.Annex2OfSMTA2, true, cancellationToken, transaction);


                    await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);
                    await SetNewWorklistFromBioHubItemLaboratoryFocalPoint(worklistFromBioHubItem, command, cancellationToken, transaction);
                    await SetNewAnnexOfSMTA2Conditions(worklistFromBioHubItem, command, true, cancellationToken, transaction);
                    await SetNewBiosafetyChecklistOfSMTA2(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;


                case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2;

                    await CancelWorklistItemFile(worklistFromBioHubItem.Id, DocumentFileType.BiosafetyChecklist, true, cancellationToken, transaction);

                    await SetNewBiosafetyChecklistOfSMTA2(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    errors = await AddBiosafetyChecklistComment(worklistFromBioHubItem, command, transaction, cancellationToken);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    worklistFromBioHubItem.Comment = command.NewBiosafetyChecklistThreadComment;

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;

                case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2;

                    await CancelWorklistItemFile(worklistFromBioHubItem.Id, DocumentFileType.BookingFormOfSMTA2, true, cancellationToken, transaction);

                    await _worklistFromBioHubItemWriteRepository.LinkBookingForm(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;


                case WorklistFromBioHubStatus.WaitForPickUpCompleted:
                    
                    await _worklistFromBioHubItemWriteRepository.UpdateBookingFormDeliveryProperties(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    worklistFromBioHubItem.LastSubmissionApproved = true;

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;


                case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:

                    if (command.IsSaveDraft != true)
                    {
                        worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForCommentQESendFeedback;


                        if (worklistFromBioHubItem.LastSubmissionApproved == false)
                        {
                            errors = await AddFeedback(worklistFromBioHubItem, command, transaction, cancellationToken);
                            if (errors.HasValue)
                                throw new Exception(errors.Value.ToString());
                        }
                        worklistFromBioHubItem.LastSubmissionApproved = true;
                    }

                    await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    break;


                case WorklistFromBioHubStatus.WaitForFinalApproval:

                    worklistFromBioHubItem.LastSubmissionApproved = true;

                    worklistFromBioHubItem.Status = WorklistFromBioHubStatus.WaitForCommentQESendFeedback;

                    errors = await AddFeedback(worklistFromBioHubItem, command, transaction, cancellationToken);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    await SetNewMaterials(worklistFromBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistFromBioHubItemWriteRepository.Update(worklistFromBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;


                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return worklistFromBioHubItem;
        }

        public async Task UpdateQEShipmentDocuments(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            switch (command.ShipmentDocumentOperationType)
            {
                case ShipmentDocumentOperationType.Add:
                    if (command.File != null)
                    {
                        var replaceExistingType = command.DocumentTemplateFileType == DocumentFileType.Other ? false : true;
                        await SetNewDocument(worklistFromBioHubItem, command, null, true, true, cancellationToken, transaction, replaceExistingType);
                    }
                    break;
                case ShipmentDocumentOperationType.Update:
                    await UpdateShipmentDocument(worklistFromBioHubItem, command, cancellationToken, transaction);
                    break;
                case ShipmentDocumentOperationType.Delete:
                    await DeleteShipmentDocument(worklistFromBioHubItem, command, cancellationToken, transaction);
                    break;
                default:
                    break;
            }
        }

        public async Task UpdateBHFShipmentDocuments(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            switch (command.ShipmentDocumentOperationType)
            {
                case ShipmentDocumentOperationType.Add:
                    if (command.File != null)
                    {
                        var replaceExistingType = command.DocumentTemplateFileType == DocumentFileType.Other ? false : true;
                        await SetNewDocument(worklistFromBioHubItem, command, null, true, true, cancellationToken, transaction, replaceExistingType, true);
                    }
                    break;
                case ShipmentDocumentOperationType.Update:
                    await UpdateShipmentDocument(worklistFromBioHubItem, command, cancellationToken, transaction);
                    break;
                case ShipmentDocumentOperationType.Delete:
                    await DeleteShipmentDocument(worklistFromBioHubItem, command, cancellationToken, transaction);
                    break;
                default:
                    break;
            }
        }

        private async Task<Errors?> AddFeedback(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            FeedbackDto feedback = new FeedbackDto();
            feedback.Text = command.NewFeedback;
            feedback.Date = DateTime.UtcNow;
            feedback.PostedById = worklistFromBioHubItem.LastOperationUserId;
            var errors = await _worklistFromBioHubItemWriteRepository.AddFeedback(worklistFromBioHubItem.Id, feedback, cancellationToken, transaction);
            return errors;
        }

        private async Task<Errors?> AddBiosafetyChecklistComment(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(command.NewBiosafetyChecklistThreadComment))
            {
                BiosafetyChecklistThreadCommentDto comment = new BiosafetyChecklistThreadCommentDto();
                comment.Text = command.NewBiosafetyChecklistThreadComment;
                comment.Date = DateTime.UtcNow;
                comment.PostedById = worklistFromBioHubItem.LastOperationUserId;
                var errors = await _worklistFromBioHubItemWriteRepository.AddBiosafetyChecklistComment(worklistFromBioHubItem.Id, comment, cancellationToken, transaction);
                if (errors != null)
                {
                    return errors;
                }
            }

            if (!string.IsNullOrEmpty(command.NewBiosafetyChecklistThreadCommentFromDocument))
            {
                BiosafetyChecklistThreadCommentDto comment = new BiosafetyChecklistThreadCommentDto();
                comment.Text = command.NewBiosafetyChecklistThreadCommentFromDocument;
                comment.Date = command.PreviousOperationDate;
                comment.PostedById = command.PreviousUserId;
                var errors = await _worklistFromBioHubItemWriteRepository.AddBiosafetyChecklistComment(worklistFromBioHubItem.Id, comment, cancellationToken, transaction);
                if (errors != null)
                {
                    return errors;
                }
            }
            return null;

        }

        private async Task<Errors?> SetNewDocument(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, Guid? originalDocumentTemplateId, bool documentApproved, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null, bool? replaceExistingType = true, bool? isBioHubFacilityDocument = false)
        {
            Document document = PrepareNewDocumentToUpload(worklistFromBioHubItem, command, originalDocumentTemplateId, documentApproved, isDocumentFile, isBioHubFacilityDocument);
            var fileId = document.Id.ToString() + "." + document.Extension;
            var laboratoryId = worklistFromBioHubItem.RequestInitiationToLaboratoryId.ToString();
            var bioHubFacilityId = worklistFromBioHubItem.RequestInitiationFromBioHubFacilityId.ToString();
            var documentType = command.DocumentTemplateFileType.ToString();

            var instituteId = isBioHubFacilityDocument == true ? bioHubFacilityId : laboratoryId;

            if (command.DocumentTemplateFileType == DocumentFileType.SMTA2)
            {
                fileId = $"{instituteId}/SMTA/SMTA2/{fileId}";
            }
            else
            {
                fileId = $"{instituteId}/Shipments/From BioHub/{worklistFromBioHubItem.ReferenceNumber}/{documentType}/{fileId}";
            }

            var uploadedFile = await _storageAccountUtility.UploadDocument(command.File, fileId);

            if (!uploadedFile)
            {
                throw new Exception("Error uploading file");
            }

            Either<Document, Errors> response = await _writeDocumentRepository.Create(document, cancellationToken, transaction);
            if (response.IsRight)
                return response.Right;

            Document createdDocument =
                response.Left ?? throw new Exception("This is a bug: document value must be defined");

            var linkDocumentResult = await _worklistFromBioHubItemWriteRepository.LinkDocument(worklistFromBioHubItem.Id, document.Id, command.DocumentTemplateFileType.GetValueOrDefault(), cancellationToken, isDocumentFile, transaction, replaceExistingType);

            if (linkDocumentResult != null)
            {
                throw new Exception("Error Linking new document to worklistFromBioHubItem");
            }

            return linkDocumentResult;
        }


        private MoveToNextStatusFromBioHubEngineCommand PrepareBookingForms(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command)
        {
            command.BookingForms = new List<BookingFormOfSMTADto>();

            var transportCategoryIds = command.WorklistFromBioHubItemMaterials.Select(x => x.TransportCategoryId).Distinct();

            foreach (var transportCategoryId in transportCategoryIds)
            {
                var newBookingForm = new BookingFormOfSMTADto();
                newBookingForm.Id = Guid.NewGuid();
                newBookingForm.TransportCategoryId = transportCategoryId;
                newBookingForm.WorklistItemId = worklistFromBioHubItem.Id;
                newBookingForm.TotalNumberOfVials = command.WorklistFromBioHubItemMaterials.Where(x => x.TransportCategoryId == transportCategoryId).Sum(x => x.Quantity);
                newBookingForm.TotalAmount = command.WorklistFromBioHubItemMaterials.Where(x => x.TransportCategoryId == transportCategoryId).Sum(x => (x.Quantity * (decimal)x.Amount));

                command.BookingForms.Add(newBookingForm);
            }
            return command;
        }


        private async Task<Errors?> UpdateShipmentDocument(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            Document document = await _writeDocumentRepository.ReadForUpdate(command.ShipmentDocumentId.GetValueOrDefault(), cancellationToken);

            if (document == null)
            {
                throw new Exception("Shipment Document not found");
            }

            document.Name = command.ShipmentDocumentNewName;


            var result = await _writeDocumentRepository.Update(document, cancellationToken, transaction);
            if (result != null)
            {
                throw new Exception("Error Updating Shipment Document");
            }
            return result;
        }

        private async Task<Errors?> DeleteShipmentDocument(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            Errors? result;
            Document document = await _writeDocumentRepository.ReadForUpdate(command.ShipmentDocumentId.GetValueOrDefault(), cancellationToken);

            if (document != null)
            {
                result = await _writeDocumentRepository.Delete(document.Id, cancellationToken, transaction);
                if (result != null)
                {
                    throw new Exception("Error Deleting Shipment Document");
                }
            }

            result = await _worklistFromBioHubItemWriteRepository.UnlinkDocument(worklistFromBioHubItem.Id, command.ShipmentDocumentId.GetValueOrDefault(), cancellationToken, true, transaction);
            if (result != null)
            {
                throw new Exception("Error Unlinking Shipment Document from WorklistFromBioHub item");
            }

            return result;
        }

        private async Task<Errors?> CancelWorklistItemFile(Guid worklistFromBioHubItemId, DocumentFileType type, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            var linkDocumentResult = await _worklistFromBioHubItemWriteRepository.LinkDocument(worklistFromBioHubItemId, null, type, cancellationToken, isDocumentFile, transaction);
            if (linkDocumentResult != null)
            {
                throw new Exception("Error Cancelling Link document on worklistFromBioHubItem");
            }
            return linkDocumentResult;
        }

        private Document PrepareNewDocumentToUpload(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, Guid? originalDocumentTemplateId, bool approved, bool isDocumentFile, bool? isBioHubFacilityDocument = false)
        {
            Document document = new Document();
            document.Id = Guid.NewGuid();
            document.Name = Path.GetFileNameWithoutExtension(command.File.FileName);
            document.Extension = (Path.GetExtension(command.File.FileName).Replace(".", ""));
            document.LaboratoryId = isBioHubFacilityDocument == true ? null : worklistFromBioHubItem.RequestInitiationToLaboratoryId;
            document.BioHubFacilityId = isBioHubFacilityDocument == true ? worklistFromBioHubItem.RequestInitiationFromBioHubFacilityId : null;
            document.UploadedById = command.UserId;
            document.CreationDate = DateTime.UtcNow;
            document.OperationDate = worklistFromBioHubItem.OperationDate.GetValueOrDefault();
            document.IsDocumentFile = isDocumentFile;
            document.OriginalDocumentTemplateId = originalDocumentTemplateId;
            document.Type = command.DocumentTemplateFileType;
            document.Approved = approved;

            if (isDocumentFile == false)
            {
                document.Base64String = CreateBase64Image(command.File);
            }

            return document;
        }




        private async Task<Errors?> SetNewMaterials(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, bool approved, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            Errors? linkMaterialsResult;
            if (approved)
            {
                linkMaterialsResult = await _worklistFromBioHubItemWriteRepository.LinkMaterials(worklistFromBioHubItem.Id, command.WorklistFromBioHubItemMaterials, cancellationToken, transaction);
            }
            else
            {
                linkMaterialsResult = await _worklistFromBioHubItemWriteRepository.LinkMaterials(worklistFromBioHubItem.Id, null, cancellationToken, transaction);
            }
            if (linkMaterialsResult != null)
            {
                throw new Exception("Error Linking new Materials to worklistFromBioHubItem");
            }

            return linkMaterialsResult;
        }

        private async Task<Errors?> SetNewAnnexOfSMTA2Conditions(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, bool approved, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            Errors? linkMaterialsResult;
            if (approved)
            {
                linkMaterialsResult = await _worklistFromBioHubItemWriteRepository.LinkAnnex2OfSMTA2Conditions(worklistFromBioHubItem.Id, command.WorklistFromBioHubItemAnnex2OfSMTA2Conditions, cancellationToken, transaction);
            }
            else
            {
                linkMaterialsResult = await _worklistFromBioHubItemWriteRepository.LinkAnnex2OfSMTA2Conditions(worklistFromBioHubItem.Id, null, cancellationToken, transaction);
            }
            if (linkMaterialsResult != null)
            {
                throw new Exception("Error Linking new AnnexOfSMTA2Conditions to worklistFromBioHubItem");
            }

            return linkMaterialsResult;
        }


        private async Task<Errors?> SetNewBiosafetyChecklistOfSMTA2(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, bool approved, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            Errors? linkMaterialsResult;
            if (approved)
            {
                linkMaterialsResult = await _worklistFromBioHubItemWriteRepository.LinkBiosafetyChecklistOfSMTA2(worklistFromBioHubItem.Id, command.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s, cancellationToken, transaction);
            }
            else
            {
                linkMaterialsResult = await _worklistFromBioHubItemWriteRepository.LinkBiosafetyChecklistOfSMTA2(worklistFromBioHubItem.Id, null, cancellationToken, transaction);
            }
            if (linkMaterialsResult != null)
            {
                throw new Exception("Error Linking new Materials to worklistFromBioHubItem");
            }

            return linkMaterialsResult;
        }

        private async Task<Errors?> SetNewWorklistFromBioHubItemLaboratoryFocalPoint(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {

            var linkWorklistFromBioHubItemLaboratoryFocalPointResult = await _worklistFromBioHubItemWriteRepository.LinkLaboratoryFocalPoints(worklistFromBioHubItem.Id, command.WorklistFromBioHubItemLaboratoryFocalPoints, cancellationToken, transaction);

            if (linkWorklistFromBioHubItemLaboratoryFocalPointResult != null)
            {
                throw new Exception("Error Linking new WorklistFromBioHubItemLaboratoryFocalPoint to worklistFromBioHubItem");
            }

            return linkWorklistFromBioHubItemLaboratoryFocalPointResult;
        }

        private async Task<Errors?> SetNewBookingForms(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, bool approved, CancellationToken cancellationToken,
           IDbContextTransaction? transaction = null)
        {
            Errors? linkBookingFromResult;
            if (approved)
            {
                linkBookingFromResult = await _worklistFromBioHubItemWriteRepository.LinkBookingForm(worklistFromBioHubItem.Id, command.BookingForms, cancellationToken, transaction);
            }
            else
            {
                linkBookingFromResult = await _worklistFromBioHubItemWriteRepository.LinkBookingForm(worklistFromBioHubItem.Id, null, cancellationToken, transaction);
            }
            if (linkBookingFromResult != null)
            {
                throw new Exception("Error Linking new BookingForm to worklistFromBioHubItem");
            }

            return linkBookingFromResult;
        }

        private string CreateBase64Image(IFormFile file)
        {
            string base64String = string.Empty;
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    base64String = Convert.ToBase64String(fileBytes);

                }
            }
            return base64String;
        }

        private Shipment CreateNewShipment(WorklistFromBioHubItem entity)
        {
            var shipment = new Shipment();
            shipment.Id = Guid.NewGuid();
            shipment.WorklistFromBioHubItemId = entity.Id;
            shipment.ReferenceNumber = entity.ReferenceNumber ?? "";
            shipment.QELaboratoryId = entity.RequestInitiationToLaboratoryId;
            shipment.BioHubFacilityId = entity.RequestInitiationFromBioHubFacilityId;
            shipment.StatusOfRequest = "Completed";
            shipment.CreationDate = DateTime.UtcNow;
            shipment.CompletedOn = entity.OperationDate;
            return shipment;
        }

    }
}