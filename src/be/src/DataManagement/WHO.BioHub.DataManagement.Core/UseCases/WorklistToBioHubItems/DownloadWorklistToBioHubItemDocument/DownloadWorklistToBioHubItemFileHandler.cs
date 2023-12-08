using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DownloadWorklistToBioHubItemFile;

public interface IDownloadWorklistToBioHubItemFileHandler
{
    Task<Either<DownloadWorklistToBioHubItemFileQueryResponse, Errors>> Handle(DownloadWorklistToBioHubItemFileQuery query, CancellationToken cancellationToken);
}

public class DownloadWorklistToBioHubItemFileHandler : IDownloadWorklistToBioHubItemFileHandler
{
    private readonly ILogger<DownloadWorklistToBioHubItemFileHandler> _logger;
    private readonly DownloadWorklistToBioHubItemFileQueryValidator _validator;
    private readonly IDocumentReadRepository _documentReadRepository;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;
    private readonly IWorklistToBioHubItemReadRepository _worklistToBioHubItemReadRepository;

    public DownloadWorklistToBioHubItemFileHandler(
        ILogger<DownloadWorklistToBioHubItemFileHandler> logger,
        DownloadWorklistToBioHubItemFileQueryValidator validator,
        IDocumentReadRepository documentReadRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IWorklistToBioHubItemReadRepository worklistToBioHubItemReadRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _documentReadRepository = documentReadRepository;
        _storageAccountUtility = storageAccountUtility;
        _worklistToBioHubItemReadRepository = worklistToBioHubItemReadRepository;
        _documentTemplateReadRepository = documentTemplateReadRepository;
    }

    public async Task<Either<DownloadWorklistToBioHubItemFileQueryResponse, Errors>> Handle(
        DownloadWorklistToBioHubItemFileQuery query,
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
            var worklistItem = await _worklistToBioHubItemReadRepository.Read(query.WorklistId, cancellationToken);

            if (worklistItem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"WorklistToBioHubItem with Id {query.Id} not found"));
            }

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklistItem.RequestInitiationFromLaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

                case RoleType.BioHubFacility:
                    if (worklistItem.RequestInitiationToBioHubFacilityId != query.UserBioHubFacilityId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;

            }
            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Download forbidden"));
            }


            if (worklistItem.Status == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1 || worklistItem.Status == WorklistToBioHubStatus.SubmitBookingFormOfSMTA1)
            {
                DocumentTemplate documentTemplate = await _documentTemplateReadRepository.Read(query.Id, cancellationToken);
                if (documentTemplate == null)
                    return new(new Errors(ErrorType.NotFound, $"Document with Id {query.Id} not found"));
                fileId = documentTemplate.Id.ToString() + "." + documentTemplate.Extension;
                documentName = documentTemplate.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocumentTemplate(fileId, documentName);
            }
            //else if (worklistItem.Status == WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments)
            //{
            //    Document document = await _documentReadRepository.Read(query.Id, cancellationToken);
            //    if (document != null)
            //    {
            //        fileId = document.LaboratoryId.ToString() + "/Shipments/Into BioHub/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
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


                fileId = $"{document.LaboratoryId.ToString()}/Shipments/Into BioHub/{worklistItem.ReferenceNumber}/{document.Type.ToString()}/{document.Id.ToString()}.{document.Extension}";

                documentName = document.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, documentName);
            }

            if (downloadedFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"File for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new DownloadWorklistToBioHubItemFileQueryResponse(DownloadedFile: downloadedFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Document with Id {id}", query.Id);
            throw;
        }
    }
}