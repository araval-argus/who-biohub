using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DownloadSMTA1WorkflowHistoryItemFile;

public interface IDownloadSMTA1WorkflowHistoryItemFileHandler
{
    Task<Either<DownloadSMTA1WorkflowHistoryItemFileQueryResponse, Errors>> Handle(DownloadSMTA1WorkflowHistoryItemFileQuery query, CancellationToken cancellationToken);
}

public class DownloadSMTA1WorkflowHistoryItemFileHandler : IDownloadSMTA1WorkflowHistoryItemFileHandler
{
    private readonly ILogger<DownloadSMTA1WorkflowHistoryItemFileHandler> _logger;
    private readonly DownloadSMTA1WorkflowHistoryItemFileQueryValidator _validator;
    private readonly IDocumentReadRepository _documentReadRepository;
    private readonly IDocumentTemplateReadRepository _documentTemplateReadRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;
    private readonly ISMTA1WorkflowHistoryItemReadRepository _worklistToBioHubHistoryItemReadRepository;

    public DownloadSMTA1WorkflowHistoryItemFileHandler(
        ILogger<DownloadSMTA1WorkflowHistoryItemFileHandler> logger,
        DownloadSMTA1WorkflowHistoryItemFileQueryValidator validator,
        IDocumentReadRepository documentReadRepository,
        IDocumentTemplateReadRepository documentTemplateReadRepository,
        ISMTA1WorkflowHistoryItemReadRepository worklistToBioHubHistoryItemReadRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _documentReadRepository = documentReadRepository;
        _storageAccountUtility = storageAccountUtility;
        _worklistToBioHubHistoryItemReadRepository = worklistToBioHubHistoryItemReadRepository;
        _documentTemplateReadRepository = documentTemplateReadRepository;
    }

    public async Task<Either<DownloadSMTA1WorkflowHistoryItemFileQueryResponse, Errors>> Handle(
        DownloadSMTA1WorkflowHistoryItemFileQuery query,
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
                return new(new Errors(ErrorType.NotFound, $"SMTA1WorkflowHistoryItem with Id {query.Id} not found"));
            }

            switch (query.RoleType)
            {
                case RoleType.Laboratory:
                    if (worklistItem.LaboratoryId != query.UserLaboratoryId)
                    {
                        return new(new Errors(ErrorType.NotFound, $"Item with Id {query.Id} not found"));
                    }
                    break;
            }

            var requiredPermission = StatusPermissionMapper.SMTA1WorkflowStatusReadPermissionMapper[worklistItem.Status];

            if (!query.UserPermissions.Contains(requiredPermission))
            {
                return new(new Errors(ErrorType.Forbidden, $"Download forbidden"));
            }


            if (worklistItem.Status == SMTA1WorkflowStatus.SubmitSMTA1)
            {
                DocumentTemplate documentTemplate = await _documentTemplateReadRepository.Read(query.Id, cancellationToken);
                if (documentTemplate == null)
                    return new(new Errors(ErrorType.NotFound, $"Document with Id {query.Id} not found"));
                fileId = documentTemplate.Id.ToString() + "." + documentTemplate.Extension;
                documentName = documentTemplate.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocumentTemplate(fileId, documentName);
            }



            else
            {
                Document document = await _documentReadRepository.Read(query.Id, cancellationToken);
                if (document == null)
                    return new(new Errors(ErrorType.NotFound, $"Document with Id {query.Id} not found"));

                fileId = document.LaboratoryId.ToString() + "/SMTA/" + document.Type.ToString() + "/" + document.Id.ToString() + "." + document.Extension;

                documentName = document.Name;
                downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, documentName);
            }


            if (downloadedFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"File for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new DownloadSMTA1WorkflowHistoryItemFileQueryResponse(DownloadedFile: downloadedFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Document with Id {id}", query.Id);
            throw;
        }
    }
}