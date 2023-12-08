using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DownloadWorklistFromBioHubItemFile;

public interface IDownloadWorklistFromBioHubItemFileHandler
{
    Task<Either<DownloadWorklistFromBioHubItemFileQueryResponse, Errors>> Handle(DownloadWorklistFromBioHubItemFileQuery query, CancellationToken cancellationToken);
}

public class DownloadWorklistFromBioHubItemFileHandler : IDownloadWorklistFromBioHubItemFileHandler
{
    private readonly ILogger<DownloadWorklistFromBioHubItemFileHandler> _logger;
    private readonly DownloadWorklistFromBioHubItemFileQueryValidator _validator;
    private readonly IDocumentReadRepository _documentReadRepository;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;
    private readonly IWorklistFromBioHubItemReadRepository _worklistFromBioHubItemReadRepository;

    public DownloadWorklistFromBioHubItemFileHandler(
        ILogger<DownloadWorklistFromBioHubItemFileHandler> logger,
        DownloadWorklistFromBioHubItemFileQueryValidator validator,
        IDocumentReadRepository documentReadRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IWorklistFromBioHubItemReadRepository worklistFromBioHubItemReadRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _documentReadRepository = documentReadRepository;
        _storageAccountUtility = storageAccountUtility;
        _worklistFromBioHubItemReadRepository = worklistFromBioHubItemReadRepository;
        _documentTemplateReadRepository = documentTemplateReadRepository;
    }

    public async Task<Either<DownloadWorklistFromBioHubItemFileQueryResponse, Errors>> Handle(
        DownloadWorklistFromBioHubItemFileQuery query,
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
            var worklistItem = await _worklistFromBioHubItemReadRepository.Read(query.WorklistId, cancellationToken);

            if (worklistItem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"WorklistFromBioHubItem with Id {query.Id} not found"));
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
                    if (worklistItem.RequestInitiationFromBioHubFacilityId != query.UserBioHubFacilityId)
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
                
            //    string instituteId = document.LaboratoryId != null ? document.LaboratoryId.ToString() : document.BioHubFacilityId.ToString();

            //    if (document != null)
            //    {
            //        fileId = instituteId + "/Shipments/From BioHub/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
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

                string instituteId = document.LaboratoryId != null ? document.LaboratoryId.ToString() : document.BioHubFacilityId.ToString();


                if (document.Type == DocumentFileType.SMTA2)
                {
                    fileId = instituteId + "/SMTA/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
                }
                else
                {
                    fileId = $"{instituteId}/Shipments/From BioHub/{worklistItem.ReferenceNumber}/{document.Type.ToString()}/{document.Id.ToString()}.{document.Extension}";

                }
                documentName = document.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, documentName);
            }

            if (downloadedFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"File for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new DownloadWorklistFromBioHubItemFileQueryResponse(DownloadedFile: downloadedFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Document with Id {id}", query.Id);
            throw;
        }
    }
}