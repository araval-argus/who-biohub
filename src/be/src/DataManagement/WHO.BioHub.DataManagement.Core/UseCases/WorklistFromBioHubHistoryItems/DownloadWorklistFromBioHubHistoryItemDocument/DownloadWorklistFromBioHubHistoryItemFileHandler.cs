using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DownloadWorklistFromBioHubHistoryItemFile;

public interface IDownloadWorklistFromBioHubHistoryItemFileHandler
{
    Task<Either<DownloadWorklistFromBioHubHistoryItemFileQueryResponse, Errors>> Handle(DownloadWorklistFromBioHubHistoryItemFileQuery query, CancellationToken cancellationToken);
}

public class DownloadWorklistFromBioHubHistoryItemFileHandler : IDownloadWorklistFromBioHubHistoryItemFileHandler
{
    private readonly ILogger<DownloadWorklistFromBioHubHistoryItemFileHandler> _logger;
    private readonly DownloadWorklistFromBioHubHistoryItemFileQueryValidator _validator;
    private readonly IDocumentReadRepository _documentReadRepository;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;
    private readonly IWorklistFromBioHubHistoryItemReadRepository _worklistFromBioHubHistoryItemReadRepository;

    public DownloadWorklistFromBioHubHistoryItemFileHandler(
        ILogger<DownloadWorklistFromBioHubHistoryItemFileHandler> logger,
        DownloadWorklistFromBioHubHistoryItemFileQueryValidator validator,
        IDocumentReadRepository documentReadRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IWorklistFromBioHubHistoryItemReadRepository worklistFromBioHubHistoryItemReadRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _documentReadRepository = documentReadRepository;
        _storageAccountUtility = storageAccountUtility;
        _worklistFromBioHubHistoryItemReadRepository = worklistFromBioHubHistoryItemReadRepository;
        _documentTemplateReadRepository = documentTemplateReadRepository;
    }

    public async Task<Either<DownloadWorklistFromBioHubHistoryItemFileQueryResponse, Errors>> Handle(
        DownloadWorklistFromBioHubHistoryItemFileQuery query,
        CancellationToken cancellationToken)
    {
        string fileId = string.Empty;
        string documentName = string.Empty;
        Either<HttpResponseMessage, Errors> downloadedFile;

        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));


        try
        {
            var worklistItem = await _worklistFromBioHubHistoryItemReadRepository.Read(query.WorklistId, cancellationToken);

            if (worklistItem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"WorklistFromBioHubHistoryItem with Id {query.Id} not found"));
            }

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklistItem.RequestInitiationToLaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklistItem.RequestInitiationFromBioHubFacilityId != query.UserBioHubFacilityId && worklistItem.Status != WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }

            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Download forbidden"));
            }


            if (worklistItem.Status == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2 || worklistItem.Status == WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2 || worklistItem.Status == WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2)
            {
                DocumentTemplate documentTemplate = await _documentTemplateReadRepository.Read(query.Id, cancellationToken);
                if (documentTemplate == null)
                    return new(new Errors(ErrorType.NotFound, $"Document with Id {query.Id} not found"));
                fileId = documentTemplate.Id.ToString() + "." + documentTemplate.Extension;
                documentName = documentTemplate.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocumentTemplate(fileId, documentName);
            }
            //else if (worklistItem.Status == WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments || worklistItem.Status == WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments)
            //{
            //    Document document = await _documentReadRepository.Read(query.Id, cancellationToken);
            //    if (document != null)
            //    {
            //        fileId = document.LaboratoryId.ToString() + "/Shipments/From BioHub/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
            //        documentName = document.Name;
            //        downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, documentName);
            //    }
            //    else
            //    {
            //        DocumentTemplate documentTemplate = await _documentTemplateReadRepository.Read(query.Id, cancellationToken);
            //        if (documentTemplate == null)
            //            return new(new Errors(ErrorType.NotFound, $"Document with Id {query.Id} not found"));
            //        fileId = documentTemplate.Id.ToString() + "." + documentTemplate.Extension;
            //        documentName = documentTemplate.Name;
            //        downloadedFile = await _storageAccountUtility.DownloadDocumentTemplate(fileId, documentName);
            //    }
            //}
            else
            {
                Document document = await _documentReadRepository.Read(query.Id, cancellationToken);
                if (document == null)
                    return new(new Errors(ErrorType.NotFound, $"Document with Id {query.Id} not found"));
                if (document.Type == DocumentFileType.SMTA2)
                {
                    fileId = document.LaboratoryId.ToString() + "/SMTA/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
                }
                else
                {
                    fileId = document.LaboratoryId.ToString() + "/Shipments/From BioHub/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
                }
                documentName = document.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, documentName);
            }


            if (downloadedFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"File for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new DownloadWorklistFromBioHubHistoryItemFileQueryResponse(DownloadedFile: downloadedFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Document with Id {id}", query.Id);
            throw;
        }
    }
}