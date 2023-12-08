using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public class SMTA2WorkflowEngine : ISMTA2WorkflowEngine
    {
        private readonly ISMTA2WorkflowItemWriteRepository _SMTA2WorkflowItemWriteRepository;
        private readonly IStorageAccountUtility _storageAccountUtility;
        private readonly IDocumentWriteRepository _writeDocumentRepository;
        private readonly IDocumentReadRepository _readDocumentRepository;

        public SMTA2WorkflowEngine(
            ISMTA2WorkflowItemWriteRepository SMTA2WorkflowItemWriteRepository,
            IStorageAccountUtility storageAccountUtility,            IDocumentWriteRepository writeDocumentRepository,            IDocumentReadRepository readDocumentRepository
        )
        {
            _SMTA2WorkflowItemWriteRepository = SMTA2WorkflowItemWriteRepository;
            _storageAccountUtility = storageAccountUtility;
            _writeDocumentRepository = writeDocumentRepository;
            _readDocumentRepository = readDocumentRepository;
        }


        public async Task<SMTA2WorkflowItem> MoveToNextStatusUponApproveOrSaveDraft(
            SMTA2WorkflowItem SMTA2WorkflowItem,
            MoveToNextStatusSMTA2WorkflowEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null
        )
        {
            SMTA2WorkflowStatus currentStatus = SMTA2WorkflowItem.Status;
            Errors? errors;
            bool isDocumentFile;
            DocumentFileType documentType;
            Guid laboratoryId;
            bool canSkipSMTA2Phase = false;


            switch (currentStatus)
            {
                case SMTA2WorkflowStatus.RequestInitiation:

                    SMTA2WorkflowItem.Status = SMTA2WorkflowStatus.SubmitSMTA2;

                    var result = await _SMTA2WorkflowItemWriteRepository.Create(SMTA2WorkflowItem, cancellationToken, transaction);
                    if (result.IsRight)
                        throw new Exception(result.Right.ToString());

                    break;


                case SMTA2WorkflowStatus.SubmitSMTA2:
                    laboratoryId = SMTA2WorkflowItem.LaboratoryId.GetValueOrDefault();
                    canSkipSMTA2Phase = false;
                    documentType = DocumentFileType.SMTA2;

                    if (SMTA2WorkflowItem.IsPast != true)
                    {
                        canSkipSMTA2Phase = await _readDocumentRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
                    }
                    if (canSkipSMTA2Phase)
                    {
                        SMTA2WorkflowItem.Status = SMTA2WorkflowStatus.SMTA2WorkflowComplete;
                    }
                    else
                    {
                        SMTA2WorkflowItem.Status = SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval;
                        await SetNewDocument(SMTA2WorkflowItem, command, command.OriginalDocumentTemplateSMTA2DocumentId, false, true, cancellationToken, transaction);
                    }

                    errors = await _SMTA2WorkflowItemWriteRepository.Update(SMTA2WorkflowItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());
                    break;




                case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:

                    laboratoryId = SMTA2WorkflowItem.LaboratoryId.GetValueOrDefault();
                    canSkipSMTA2Phase = false;
                    documentType = DocumentFileType.SMTA2;
                    if (SMTA2WorkflowItem.IsPast != true)
                    {
                        canSkipSMTA2Phase = await _readDocumentRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
                    }
                    if (canSkipSMTA2Phase)
                    {
                        SMTA2WorkflowItem.Status = SMTA2WorkflowStatus.SMTA2WorkflowComplete;
                    }
                    else
                    {
                        SMTA2WorkflowItem.Status = SMTA2WorkflowStatus.SMTA2WorkflowComplete;
                        await SetNewDocument(SMTA2WorkflowItem, command, command.OriginalDocumentTemplateSMTA2DocumentId, true, true, cancellationToken, transaction);

                    }

                    errors = await _SMTA2WorkflowItemWriteRepository.Update(SMTA2WorkflowItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;


                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return SMTA2WorkflowItem;
        }


        public async Task<SMTA2WorkflowItem> MoveToNextStatusUponReject(SMTA2WorkflowItem SMTA2WorkflowItem,
            MoveToNextStatusSMTA2WorkflowEngineCommand command,
            CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            SMTA2WorkflowStatus currentStatus = SMTA2WorkflowItem.Status;
            Errors? errors;

            switch (currentStatus)
            {

                case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:
                    SMTA2WorkflowItem.Status = SMTA2WorkflowStatus.SubmitSMTA2;
                    await CancelWorklistItemFile(SMTA2WorkflowItem.Id, DocumentFileType.SMTA2, true, cancellationToken, transaction);
                    errors = await _SMTA2WorkflowItemWriteRepository.Update(SMTA2WorkflowItem, cancellationToken, transaction);
                    if (errors.HasValue)
                        throw new Exception(errors.Value.ToString());

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus));
            }

            return SMTA2WorkflowItem;
        }




        private async Task<Errors?> SetNewDocument(SMTA2WorkflowItem SMTA2WorkflowItem, MoveToNextStatusSMTA2WorkflowEngineCommand command, Guid? originalDocumentTemplateId, bool documentApproved, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null, bool? replaceExistingType = true)
        {
            Document document = PrepareNewDocumentToUpload(SMTA2WorkflowItem, command, originalDocumentTemplateId, documentApproved, isDocumentFile);
            var fileId = document.Id.ToString() + "." + document.Extension;
            var laboratoryId = SMTA2WorkflowItem.LaboratoryId.ToString();

            fileId = laboratoryId + "/SMTA/SMTA2/" + fileId;

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

            var linkDocumentResult = await _SMTA2WorkflowItemWriteRepository.LinkDocument(SMTA2WorkflowItem.Id, document.Id, command.DocumentTemplateFileType.GetValueOrDefault(), cancellationToken, isDocumentFile, transaction, replaceExistingType);

            if (linkDocumentResult != null)
            {
                throw new Exception("Error Linking new document to SMTA2WorkflowItem");
            }

            return linkDocumentResult;
        }


        private async Task<Errors?> CancelWorklistItemFile(Guid SMTA2WorkflowItemId, DocumentFileType type, bool isDocumentFile, CancellationToken cancellationToken,
            IDbContextTransaction? transaction = null)
        {
            var linkDocumentResult = await _SMTA2WorkflowItemWriteRepository.LinkDocument(SMTA2WorkflowItemId, null, type, cancellationToken, isDocumentFile, transaction);
            if (linkDocumentResult != null)
            {
                throw new Exception("Error Cancelling Link document on SMTA2WorkflowItem");
            }
            return linkDocumentResult;
        }

        private Document PrepareNewDocumentToUpload(SMTA2WorkflowItem SMTA2WorkflowItem, MoveToNextStatusSMTA2WorkflowEngineCommand command, Guid? originalDocumentTemplateId, bool approved, bool isDocumentFile)
        {
            Document document = new Document();
            document.Id = Guid.NewGuid();
            document.Name = Path.GetFileNameWithoutExtension(command.File.FileName);
            document.Extension = (Path.GetExtension(command.File.FileName).Replace(".", ""));
            document.LaboratoryId = SMTA2WorkflowItem.LaboratoryId;

            document.UploadedById = command.UserId;
            document.CreationDate = DateTime.UtcNow;
            document.OperationDate = SMTA2WorkflowItem.OperationDate.GetValueOrDefault();
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