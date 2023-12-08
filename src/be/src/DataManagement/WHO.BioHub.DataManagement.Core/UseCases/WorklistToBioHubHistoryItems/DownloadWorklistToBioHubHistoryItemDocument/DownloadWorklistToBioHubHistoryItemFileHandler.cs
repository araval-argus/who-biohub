using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DownloadWorklistToBioHubHistoryItemFile;

public interface IDownloadWorklistToBioHubHistoryItemFileHandler
{
    Task<Either<DownloadWorklistToBioHubHistoryItemFileQueryResponse, Errors>> Handle(DownloadWorklistToBioHubHistoryItemFileQuery query, CancellationToken cancellationToken);
}

public class DownloadWorklistToBioHubHistoryItemFileHandler : IDownloadWorklistToBioHubHistoryItemFileHandler
{
    private readonly ILogger<DownloadWorklistToBioHubHistoryItemFileHandler> _logger;
    private readonly DownloadWorklistToBioHubHistoryItemFileQueryValidator _validator;
    private readonly IDocumentReadRepository _documentReadRepository;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;
    private readonly IWorklistToBioHubHistoryItemReadRepository _worklistToBioHubHistoryItemReadRepository;

    public DownloadWorklistToBioHubHistoryItemFileHandler(
        ILogger<DownloadWorklistToBioHubHistoryItemFileHandler> logger,
        DownloadWorklistToBioHubHistoryItemFileQueryValidator validator,
        IDocumentReadRepository documentReadRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        IWorklistToBioHubHistoryItemReadRepository worklistToBioHubHistoryItemReadRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _documentReadRepository = documentReadRepository;
        _storageAccountUtility = storageAccountUtility;
        _worklistToBioHubHistoryItemReadRepository = worklistToBioHubHistoryItemReadRepository;
        _documentTemplateReadRepository = documentTemplateReadRepository;
    }

    public async Task<Either<DownloadWorklistToBioHubHistoryItemFileQueryResponse, Errors>> Handle(
        DownloadWorklistToBioHubHistoryItemFileQuery query,
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
            var worklistItem = await _worklistToBioHubHistoryItemReadRepository.Read(query.WorklistId, cancellationToken);

            if (worklistItem == null)
            {
                return new(new Errors(ErrorType.NotFound, $"WorklistToBioHubHistoryItem with Id {query.Id} not found"));
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
                    if (worklistItem.RequestInitiationToBioHubFacilityId != query.UserBioHubFacilityId && worklistItem.Status != WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval)
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


            //else if (worklistItem.Status == WorklistToBioHubStatus.WaitingForSMTA1BHFsApproval || worklistItem.Status == WorklistToBioHubStatus.WaitingForSMTA1SECsApproval)
            //{
            //    Document document = await _documentReadRepository.Read(query.Id, cancellationToken);
            //    if (document != null)
            //    {
            //        fileId = document.LaboratoryId.ToString() + "/SMTA/SMTA1/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
            //        documentName = document.Name;
            //        downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, documentName);
            //    }
            //}

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
                if (document.Type == DocumentFileType.SMTA1)
                {
                    fileId = document.LaboratoryId.ToString() + "/SMTA/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
                }
                else
                {
                    fileId = document.LaboratoryId.ToString() + "/Shipments/Into BioHub/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;
                }
                documentName = document.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, documentName);
            }


            if (downloadedFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"File for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new DownloadWorklistToBioHubHistoryItemFileQueryResponse(DownloadedFile: downloadedFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Document with Id {id}", query.Id);
            throw;
        }
    }
}