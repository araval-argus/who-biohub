using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public class SMTA1WorkflowEngine : ISMTA1WorkflowEngine
    {

        private readonly ISMTA1WorkflowItemWriteRepository _SMTA1WorkflowItemWriteRepository;
        private readonly IStorageAccountUtility _storageAccountUtility;
        private readonly IDocumentWriteRepository _writeDocumentRepository;
        private readonly IDocumentReadRepository _readDocumentRepository;


        public SMTA1WorkflowEngine(
            ISMTA1WorkflowItemWriteRepository SMTA1WorkflowItemWriteRepository,
            IStorageAccountUtility storageAccountUtility,            IDocumentWriteRepository writeDocumentRepository,            IDocumentReadRepository readDocumentRepository
        )
        {
            _SMTA1WorkflowItemWriteRepository = SMTA1WorkflowItemWriteRepository;
            _storageAccountUtility = storageAccountUtility;
            _writeDocumentRepository = writeDocumentRepository;
            _readDocumentRepository = readDocumentRepository;
        }


        public async Task<SMTA1WorkflowItem> MoveToNextStatusUponApproveOrSaveDraft(
            SMTA1WorkflowItem SMTA1WorkflowItem,
            MoveToNextStatusSMTA1WorkflowEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null
        )
        {
            SMTA1WorkflowStatus currentStatus = SMTA1WorkflowItem.Status;
            Errors? errors;
            bool isDocumentFile;
            DocumentFileType documentType;
            Guid laboratoryId;
            Guid bioHubFacilityId;
            bool canSkipSMTA1Phase = false;


            switch (currentStatus)
            {
                case SMTA1WorkflowStatus.RequestInitiation:

                    SMTA1WorkflowItem.Status = SMTA1WorkflowStatus.SubmitSMTA1;

                    var result = await _SMTA1WorkflowItemWriteRepository.Create(SMTA1WorkflowItem, cancellationToken, transaction);
                    if (result.IsRight)
                        throw new Exception(result.Right.ToString());

                    break;


                case SMTA1WorkflowStatus.SubmitSMTA1:
                    laboratoryId = SMTA1WorkflowItem.LaboratoryId.GetValueOrDefault();

                    canSkipSMTA1Phase = false;
                    documentType = DocumentFileType.SMTA1;
                    if (SMTA1WorkflowItem.IsPast != true)
                    {
                        canSkipSMTA1Phase = await _readDocumentRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
                    }
                    if (canSkipSMTA1Phase)
                    {
                        SMTA1WorkflowItem.Status = SMTA1WorkflowStatus.SMTA1WorkflowComplete;
                    }
                    else
                    {
                        SMTA1WorkflowItem.Status = SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval;
                        await SetNewDocument(SMTA1WorkflowItem, command, command.OriginalDocumentTemplateSMTA1DocumentId, false, true, cancellationToken, transaction);
                    }

                    errors = await _SMTA1WorkflowItemWriteRepository.Update(SMTA1WorkflowItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;


                case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:

                    laboratoryId = SMTA1WorkflowItem.LaboratoryId.GetValueOrDefault();
                    canSkipSMTA1Phase = false;
                    documentType = DocumentFileType.SMTA1;
                    if (SMTA1WorkflowItem.IsPast != true)
                    {
                        canSkipSMTA1Phase = await _readDocumentRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
                    }
                    if (canSkipSMTA1Phase)
                    {
                        SMTA1WorkflowItem.Status = SMTA1WorkflowStatus.SMTA1WorkflowComplete;
                    }
                    else
                    {
                        SMTA1WorkflowItem.Status = SMTA1WorkflowStatus.SMTA1WorkflowComplete;
                        await SetNewDocument(SMTA1WorkflowItem, command, command.OriginalDocumentTemplateSMTA1DocumentId, true, true, cancellationToken, transaction);
                    }


                    errors = await _SMTA1WorkflowItemWriteRepository.Update(SMTA1WorkflowItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;


                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return SMTA1WorkflowItem;
        }


        public async Task<SMTA1WorkflowItem> MoveToNextStatusUponReject(SMTA1WorkflowItem SMTA1WorkflowItem,
            MoveToNextStatusSMTA1WorkflowEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            SMTA1WorkflowStatus currentStatus = SMTA1WorkflowItem.Status;
            Errors? errors;

            switch (currentStatus)
            {

                case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:
                    SMTA1WorkflowItem.Status = SMTA1WorkflowStatus.SubmitSMTA1;
                    await CancelWorklistItemFile(SMTA1WorkflowItem.Id, DocumentFileType.SMTA1, true, cancellationToken, transaction);
                    errors = await _SMTA1WorkflowItemWriteRepository.Update(SMTA1WorkflowItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return SMTA1WorkflowItem;
        }




        private async Task<Errors?> SetNewDocument(SMTA1WorkflowItem SMTA1WorkflowItem, MoveToNextStatusSMTA1WorkflowEngineCommand command, Guid? originalDocumentTemplateId, bool documentApproved, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null, bool? replaceExistingType = true)
        {
            Document document = PrepareNewDocumentToUpload(SMTA1WorkflowItem, command, originalDocumentTemplateId, documentApproved, isDocumentFile);
            var fileId = document.Id.ToString() + "." + document.Extension;
            var laboratoryId = SMTA1WorkflowItem.LaboratoryId.ToString();


            fileId = laboratoryId + "/SMTA/SMTA1/" + fileId;


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

            var linkDocumentResult = await _SMTA1WorkflowItemWriteRepository.LinkDocument(SMTA1WorkflowItem.Id, document.Id, command.DocumentTemplateFileType.GetValueOrDefault(), cancellationToken, isDocumentFile, transaction, replaceExistingType);

            if (linkDocumentResult != null)
            {
                throw new Exception("Error Linking new document to SMTA1WorkflowItem");
            }

            return linkDocumentResult;
        }


        private async Task<Errors?> CancelWorklistItemFile(Guid SMTA1WorkflowItemId, DocumentFileType type, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            var linkDocumentResult = await _SMTA1WorkflowItemWriteRepository.LinkDocument(SMTA1WorkflowItemId, null, type, cancellationToken, isDocumentFile, transaction);
            if (linkDocumentResult != null)
            {
                throw new Exception("Error Cancelling Link document on SMTA1WorkflowItem");
            }
            return linkDocumentResult;
        }

        private Document PrepareNewDocumentToUpload(SMTA1WorkflowItem SMTA1WorkflowItem, MoveToNextStatusSMTA1WorkflowEngineCommand command, Guid? originalDocumentTemplateId, bool approved, bool isDocumentFile)
        {
            Document document = new Document();
            document.Id = Guid.NewGuid();
            document.Name = Path.GetFileNameWithoutExtension(command.File.FileName);
            document.Extension = (Path.GetExtension(command.File.FileName).Replace(".", ""));
            document.LaboratoryId = SMTA1WorkflowItem.LaboratoryId;

            document.UploadedById = command.UserId;
            document.CreationDate = DateTime.UtcNow;
            document.OperationDate = SMTA1WorkflowItem.OperationDate.GetValueOrDefault();
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
    }
}