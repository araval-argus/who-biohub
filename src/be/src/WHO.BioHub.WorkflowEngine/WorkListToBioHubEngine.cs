using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public class WorklistToBioHubEngine : IWorklistToBioHubEngine
    {
        private readonly IDocumentReadRepository _documentReadRepository;
        private readonly IWorklistToBioHubItemWriteRepository _worklistToBioHubItemWriteRepository;
        private readonly IStorageAccountUtility _storageAccountUtility;
        private readonly IDocumentWriteRepository _writeDocumentRepository;
        private readonly IShipmentWriteRepository _shipmentWriteRepository;
        private readonly IWorklistItemUsedReferenceNumberWriteRepository _writeUsedReferenceNumberRepository;


        public WorklistToBioHubEngine(            IDocumentReadRepository documentReadRepository,            IWorklistToBioHubItemWriteRepository worklistToBioHubItemWriteRepository,            IStorageAccountUtility storageAccountUtility,            IDocumentWriteRepository writeDocumentRepository,            IShipmentWriteRepository shipmentWriteRepository,            IWorklistItemUsedReferenceNumberWriteRepository writeUsedReferenceNumberRepository
        )
        {
            _documentReadRepository = documentReadRepository;
            _worklistToBioHubItemWriteRepository = worklistToBioHubItemWriteRepository;
            _storageAccountUtility = storageAccountUtility;
            _writeDocumentRepository = writeDocumentRepository;
            _shipmentWriteRepository = shipmentWriteRepository;
            _writeUsedReferenceNumberRepository = writeUsedReferenceNumberRepository;
        }


        public async Task<WorklistToBioHubItem> MoveToNextStatusUponApproveOrSaveDraft(
            WorklistToBioHubItem worklistToBioHubItem,
            MoveToNextStatusToBioHubEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null
        )
        {
            WorklistToBioHubStatus currentStatus = worklistToBioHubItem.Status;
            Errors? errors;
            bool isDocumentFile;
            Shipment shipment;
            Either<Shipment, Errors> shipmentCreationResult;
            DocumentFileType documentType;
            bool canSkipSMTA1Phase;
            Guid laboratoryId;
            Guid bioHubFacilityId;


            switch (currentStatus)
            {
                case WorklistToBioHubStatus.RequestInitiation:

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1;                    

                    var worklistItemUsedReferenceNumber = await _writeUsedReferenceNumberRepository.Create(worklistToBioHubItem.IsPast, cancellationToken, transaction);
                    if (worklistItemUsedReferenceNumber.IsRight)
                        throw new Exception(worklistItemUsedReferenceNumber.Right.ToString());

                    worklistToBioHubItem.ReferenceNumber = worklistItemUsedReferenceNumber.Left.ReferenceNumber;


                    var result = await _worklistToBioHubItemWriteRepository.Create(worklistToBioHubItem, cancellationToken, transaction);
                    if (result.IsRight)
                        throw new Exception(result.Right.ToString());

                    break;

                case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:
                    if (command.IsSaveDraft != true)
                    {
                        worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval;
                    }
                    isDocumentFile = worklistToBioHubItem.Annex2FillingOption == FillingOption.DocumentUpload;

                    if (command.File != null)
                    {
                        await SetNewDocument(worklistToBioHubItem, command, command.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId, false, isDocumentFile, cancellationToken, transaction);
                    }
                    if (worklistToBioHubItem.Annex2FillingOption == FillingOption.ElectronicallyFill)
                    {
                        await SetNewMaterialShippingInformations(worklistToBioHubItem, command, true, cancellationToken, transaction);
                        await SetNewWorklistToBioHubItemLaboratoryFocalPoint(worklistToBioHubItem, command, cancellationToken, transaction);
                    }

                    if (worklistToBioHubItem.IsPast == true)
                    {
                        errors = await _worklistToBioHubItemWriteRepository.LinkDocument(worklistToBioHubItem.Id, command.CurrentDownloadSMTA1DocumentId.GetValueOrDefault(), DocumentFileType.SMTA1, cancellationToken, true, transaction, true);
                        if (errors.HasValue)
                            throw new Exception(errors.Value.ToString());
                    }

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;

                case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1;
                    command = PrepareBookingForms(worklistToBioHubItem, command);
                    worklistToBioHubItem.Annex2OfSMTA1ApprovalDate = worklistToBioHubItem.OperationDate;

                    await SetNewMaterialShippingInformations(worklistToBioHubItem, command, true, cancellationToken, transaction);
                    await SetNewWorklistToBioHubItemLaboratoryFocalPoint(worklistToBioHubItem, command, cancellationToken, transaction);
                    await _worklistToBioHubItemWriteRepository.LinkBookingForm(worklistToBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    if (worklistToBioHubItem.Annex2FillingOption == FillingOption.DocumentUpload)
                    {
                        var approveAnnex2Document = await _writeDocumentRepository.ApproveWorklistToBioHubItemDocument(worklistToBioHubItem.Id, DocumentFileType.Annex2OfSMTA1, cancellationToken, transaction);

                        if (approveAnnex2Document != null)
                        {
                            throw new Exception("Error approving Annex 2 of SMTA 1 document");
                        }
                    }

                    var linkDocumentResult = await _worklistToBioHubItemWriteRepository.LinkDocument(worklistToBioHubItem.Id, command.CurrentDownloadSMTA1DocumentId.GetValueOrDefault(), DocumentFileType.SMTA1, cancellationToken, true, transaction, true);

                    if (linkDocumentResult != null)
                    {
                        throw new Exception("Error Linking new document to worklistToBioHubItem");
                    }

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;

                case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:
                    if (command.IsSaveDraft != true)
                    {
                        worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval;
                    }

                    isDocumentFile = worklistToBioHubItem.BookingFormFillingOption == FillingOption.DocumentUpload;

                    if (command.File != null)
                    {
                        await SetNewDocument(worklistToBioHubItem, command, command.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId, false, isDocumentFile, cancellationToken, transaction);
                    }
                    if (worklistToBioHubItem.BookingFormFillingOption == FillingOption.ElectronicallyFill)
                    {
                        await _worklistToBioHubItemWriteRepository.LinkBookingForm(worklistToBioHubItem.Id, command.BookingForms, cancellationToken, transaction);
                    }

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;

                case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitForPickUpCompleted;
                    worklistToBioHubItem.BookingFormOfSMTA1ApprovalDate = worklistToBioHubItem.OperationDate;

                    await _worklistToBioHubItemWriteRepository.LinkBookingForm(worklistToBioHubItem.Id, command.BookingForms, cancellationToken, transaction);
                    await _worklistToBioHubItemWriteRepository.LinkBioHubFacilityFocalPoints(worklistToBioHubItem.Id, command.WorklistToBioHubItemBioHubFacilityFocalPoints, cancellationToken, transaction);

                    if (worklistToBioHubItem.BookingFormFillingOption == FillingOption.DocumentUpload)
                    {
                        var approveBookingFormDocument = await _writeDocumentRepository.ApproveWorklistToBioHubItemDocument(worklistToBioHubItem.Id, DocumentFileType.BookingFormOfSMTA1, cancellationToken, transaction);

                        if (approveBookingFormDocument != null)
                        {
                            throw new Exception("Error approving Booking Form of SMTA 1 document");
                        }
                    }

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;

                
                case WorklistToBioHubStatus.WaitForPickUpCompleted:

                    if (command.IsSaveDraft != true)
                    {
                        worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitForDeliveryCompleted;
                    }


                    await _worklistToBioHubItemWriteRepository.UpdateBookingFormDeliveryProperties(worklistToBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;


                case WorklistToBioHubStatus.WaitForDeliveryCompleted:

                    if (command.IsSaveDraft != true)
                    {
                        worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitForArrivalConditionCheck;
                    }

                    await _worklistToBioHubItemWriteRepository.UpdateBookingFormDeliveryProperties(worklistToBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;

                case WorklistToBioHubStatus.WaitForArrivalConditionCheck:

                    if (command.IsSaveDraft != true)
                    {
                        worklistToBioHubItem.Status = WorklistToBioHubStatus.ShipmentCompleted;
                    }

                    await SetNewMaterialShippingInformations(worklistToBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());


                    if (command.IsSaveDraft != true)
                    {
                        shipment = CreateNewShipment(worklistToBioHubItem);
                        shipmentCreationResult = await _shipmentWriteRepository.Create(shipment, cancellationToken, transaction);

                        if (shipmentCreationResult.IsRight)
                        {
                            throw new Exception(shipmentCreationResult.Right.ToString());
                        }

                        errors = await _worklistToBioHubItemWriteRepository.CreateBMEPPMaterialFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubItem.OperationDate.GetValueOrDefault(), cancellationToken, transaction);

                    }
                    break;

                case WorklistToBioHubStatus.WaitForCommentBHFSendFeedback:

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitForFinalApproval;

                    errors = await AddFeedback(worklistToBioHubItem, command, transaction, cancellationToken);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;

                case WorklistToBioHubStatus.WaitForFinalApproval:

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.ShipmentCompleted;

                    await SetNewMaterialShippingInformations(worklistToBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    shipment = CreateNewShipment(worklistToBioHubItem);
                    shipmentCreationResult = await _shipmentWriteRepository.Create(shipment, cancellationToken, transaction);

                    if (shipmentCreationResult.IsRight)
                    {
                        throw new Exception(shipmentCreationResult.Right.ToString());
                    }

                    errors = await _worklistToBioHubItemWriteRepository.CreateBMEPPMaterialFromWorklistToBioHubItem(worklistToBioHubItem.Id, worklistToBioHubItem.OperationDate.GetValueOrDefault(), cancellationToken, transaction);

                    break;


                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return worklistToBioHubItem;
        }

        public async Task UpdateShipmentDocuments(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            switch (command.ShipmentDocumentOperationType)
            {
                case ShipmentDocumentOperationType.Add:
                    if (command.File != null)
                    {
                        var replaceExistingType = command.DocumentTemplateFileType == DocumentFileType.Other ? false : true;
                        await SetNewDocument(worklistToBioHubItem, command, null, true, true, cancellationToken, transaction, replaceExistingType);
                    }
                    break;
                case ShipmentDocumentOperationType.Update:
                    await UpdateShipmentDocument(worklistToBioHubItem, command, cancellationToken, transaction);
                    break;
                case ShipmentDocumentOperationType.Delete:
                    await DeleteShipmentDocument(worklistToBioHubItem, command, cancellationToken, transaction);
                    break;
                default:
                    break;
            }
        }

        public async Task<WorklistToBioHubItem> MoveToNextStatusUponReject(WorklistToBioHubItem worklistToBioHubItem,
            MoveToNextStatusToBioHubEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            WorklistToBioHubStatus currentStatus = worklistToBioHubItem.Status;
            Errors? errors;

            switch (currentStatus)
            {

                case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.SubmitAnnex2OfSMTA1;

                    await CancelWorklistItemFile(worklistToBioHubItem.Id, DocumentFileType.Annex2OfSMTA1, true, cancellationToken, transaction);


                    await SetNewMaterialShippingInformations(worklistToBioHubItem, command, true, cancellationToken, transaction);
                    await SetNewWorklistToBioHubItemLaboratoryFocalPoint(worklistToBioHubItem, command, cancellationToken, transaction);


                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;

                case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.SubmitBookingFormOfSMTA1;

                    await CancelWorklistItemFile(worklistToBioHubItem.Id, DocumentFileType.BookingFormOfSMTA1, true, cancellationToken, transaction);

                    await _worklistToBioHubItemWriteRepository.LinkBookingForm(worklistToBioHubItem.Id, command.BookingForms, cancellationToken, transaction);

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;


                case WorklistToBioHubStatus.WaitForArrivalConditionCheck:

                    if (command.IsSaveDraft != true)
                    {
                        worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback;


                        if (worklistToBioHubItem.LastSubmissionApproved == false)
                        {
                            errors = await AddFeedback(worklistToBioHubItem, command, transaction, cancellationToken);
                            if (errors.HasValue)
                                throw new Exception(errors.Value.ToString());
                        }
                        worklistToBioHubItem.LastSubmissionApproved = true;
                    }

                    await SetNewMaterialShippingInformations(worklistToBioHubItem, command, true, cancellationToken, transaction);

                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;


                case WorklistToBioHubStatus.WaitForFinalApproval:

                    worklistToBioHubItem.LastSubmissionApproved = true;

                    worklistToBioHubItem.Status = WorklistToBioHubStatus.WaitForCommentBHFSendFeedback;

                    errors = await AddFeedback(worklistToBioHubItem, command, transaction, cancellationToken);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    await SetNewMaterialShippingInformations(worklistToBioHubItem, command, true, cancellationToken, transaction);


                    errors = await _worklistToBioHubItemWriteRepository.Update(worklistToBioHubItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;


                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return worklistToBioHubItem;
        }


        private MoveToNextStatusToBioHubEngineCommand PrepareBookingForms(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command)
        {
            command.BookingForms = new List<BookingFormOfSMTADto>();

            var transportCategoryIds = command.MaterialShippingInformations.Select(x => x.TransportCategoryId).Distinct();

            foreach (var transportCategoryId in transportCategoryIds)
            {

                var newBookingForm = new BookingFormOfSMTADto();
                newBookingForm.Id = Guid.NewGuid();
                newBookingForm.TransportCategoryId = transportCategoryId;
                newBookingForm.WorklistItemId = worklistToBioHubItem.Id;
                newBookingForm.TotalNumberOfVials = command.MaterialShippingInformations.Where(x => x.TransportCategoryId == transportCategoryId).Sum(x => x.Quantity);
                newBookingForm.TotalAmount = command.MaterialShippingInformations.Where(x => x.TransportCategoryId == transportCategoryId).Sum(x => (x.Quantity * (decimal)x.Amount));
                command.BookingForms.Add(newBookingForm);


            }

            return command;
        }

        private async Task<Errors?> AddFeedback(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            FeedbackDto feedback = new FeedbackDto();
            feedback.Text = command.NewFeedback;
            feedback.Date = DateTime.UtcNow;
            feedback.PostedById = worklistToBioHubItem.LastOperationUserId;
            var errors = await _worklistToBioHubItemWriteRepository.AddFeedback(worklistToBioHubItem.Id, feedback, cancellationToken, transaction);
            return errors;
        }

        private async Task<Errors?> SetNewDocument(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, Guid? originalDocumentTemplateId, bool documentApproved, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null, bool? replaceExistingType = true)
        {
            Document document = PrepareNewDocumentToUpload(worklistToBioHubItem, command, originalDocumentTemplateId, documentApproved, isDocumentFile);
            var fileId = document.Id.ToString() + "." + document.Extension;
            var laboratoryId = worklistToBioHubItem.RequestInitiationFromLaboratoryId.ToString();

            var documentType = command.DocumentTemplateFileType.ToString();


            if (command.DocumentTemplateFileType == DocumentFileType.SMTA1)
            {
                fileId = $"{laboratoryId}/SMTA/SMTA1/{fileId}";
            }
            else
            {
                fileId = $"{laboratoryId}/Shipments/Into BioHub/{worklistToBioHubItem.ReferenceNumber}/{documentType}/{fileId}";
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

            var linkDocumentResult = await _worklistToBioHubItemWriteRepository.LinkDocument(worklistToBioHubItem.Id, document.Id, command.DocumentTemplateFileType.GetValueOrDefault(), cancellationToken, isDocumentFile, transaction, replaceExistingType);

            if (linkDocumentResult != null)
            {
                throw new Exception("Error Linking new document to worklistToBioHubItem");
            }

            return linkDocumentResult;
        }


        private async Task<Errors?> UpdateShipmentDocument(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, CancellationToken cancellationToken,
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

        private async Task<Errors?> DeleteShipmentDocument(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, CancellationToken cancellationToken,
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

            result = await _worklistToBioHubItemWriteRepository.UnlinkDocument(worklistToBioHubItem.Id, command.ShipmentDocumentId.GetValueOrDefault(), cancellationToken, true, transaction);
            if (result != null)
            {
                throw new Exception("Error Unlinking Shipment Document from WorklistToBioHub item");
            }

            return result;
        }

        private async Task<Errors?> CancelWorklistItemFile(Guid worklistToBioHubItemId, DocumentFileType type, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            var linkDocumentResult = await _worklistToBioHubItemWriteRepository.LinkDocument(worklistToBioHubItemId, null, type, cancellationToken, isDocumentFile, transaction);
            if (linkDocumentResult != null)
            {
                throw new Exception("Error Cancelling Link document on worklistToBioHubItem");
            }
            return linkDocumentResult;
        }

        private Document PrepareNewDocumentToUpload(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, Guid? originalDocumentTemplateId, bool approved, bool isDocumentFile)
        {
            Document document = new Document();
            document.Id = Guid.NewGuid();
            document.Name = Path.GetFileNameWithoutExtension(command.File.FileName);
            document.Extension = (Path.GetExtension(command.File.FileName).Replace(".", ""));
            document.LaboratoryId = worklistToBioHubItem.RequestInitiationFromLaboratoryId;

            document.UploadedById = command.UserId;
            document.CreationDate = DateTime.UtcNow;
            document.OperationDate = worklistToBioHubItem.OperationDate.GetValueOrDefault();
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



        private async Task<Errors?> SetNewMaterialShippingInformations(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, bool approved, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            Errors? linkMaterialShippingInformationResult;
            if (approved)
            {
                linkMaterialShippingInformationResult = await _worklistToBioHubItemWriteRepository.LinkMaterialShippingInformation(worklistToBioHubItem.Id, command.MaterialShippingInformations, cancellationToken, transaction);
            }
            else
            {
                linkMaterialShippingInformationResult = await _worklistToBioHubItemWriteRepository.LinkMaterialShippingInformation(worklistToBioHubItem.Id, null, cancellationToken, transaction);
            }
            if (linkMaterialShippingInformationResult != null)
            {
                throw new Exception("Error Linking new MaterialShippingInformation to worklistToBioHubItem");
            }

            return linkMaterialShippingInformationResult;
        }

        private async Task<Errors?> SetNewWorklistToBioHubItemLaboratoryFocalPoint(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {

            var linkWorklistToBioHubItemLaboratoryFocalPointResult = await _worklistToBioHubItemWriteRepository.LinkLaboratoryFocalPoints(worklistToBioHubItem.Id, command.WorklistToBioHubItemLaboratoryFocalPoints, cancellationToken, transaction);

            if (linkWorklistToBioHubItemLaboratoryFocalPointResult != null)
            {
                throw new Exception("Error Linking new WorklistToBioHubItemLaboratoryFocalPoint to worklistToBioHubItem");
            }

            return linkWorklistToBioHubItemLaboratoryFocalPointResult;
        }

        private async Task<Errors?> SetNewBookingForms(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, bool approved, CancellationToken cancellationToken,
           IDbContextTransaction? transaction = null)
        {
            Errors? linkBookingFromResult;
            if (approved)
            {
                linkBookingFromResult = await _worklistToBioHubItemWriteRepository.LinkBookingForm(worklistToBioHubItem.Id, command.BookingForms, cancellationToken, transaction);
            }
            else
            {
                linkBookingFromResult = await _worklistToBioHubItemWriteRepository.LinkBookingForm(worklistToBioHubItem.Id, null, cancellationToken, transaction);
            }
            if (linkBookingFromResult != null)
            {
                throw new Exception("Error Linking new BookingForm to worklistToBioHubItem");
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

        private Shipment CreateNewShipment(WorklistToBioHubItem entity)
        {
            var shipment = new Shipment();
            shipment.Id = Guid.NewGuid();
            shipment.WorklistToBioHubItemId = entity.Id;
            shipment.ReferenceNumber = entity.ReferenceNumber ?? "";
            shipment.QELaboratoryId = entity.RequestInitiationFromLaboratoryId;
            shipment.BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId;
            shipment.StatusOfRequest = "Completed";
            shipment.CreationDate = DateTime.UtcNow;
            shipment.CompletedOn = entity.OperationDate;
            return shipment;
        }

    }
}