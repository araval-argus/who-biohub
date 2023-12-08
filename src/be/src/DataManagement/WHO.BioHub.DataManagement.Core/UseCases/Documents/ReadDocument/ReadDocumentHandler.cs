using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.StorageAccount;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ReadDocument;

public interface IReadDocumentHandler
{
    Task<Either<ReadDocumentQueryResponse, Errors>> Handle(ReadDocumentQuery query, CancellationToken cancellationToken);
}

public class ReadDocumentHandler : IReadDocumentHandler
{
    private readonly ILogger<ReadDocumentHandler> _logger;
    private readonly ReadDocumentQueryValidator _validator;
    private readonly IDocumentReadRepository _readRepository;
    private readonly IStorageAccountUtility _storageAccountUtility;

    public ReadDocumentHandler(
        ILogger<ReadDocumentHandler> logger,
        ReadDocumentQueryValidator validator,
        IDocumentReadRepository readRepository,
        IStorageAccountUtility storageAccountUtility)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _storageAccountUtility = storageAccountUtility;
    }

    public async Task<Either<ReadDocumentQueryResponse, Errors>> Handle(
        ReadDocumentQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Document document;

            if (query.LaboratoryId != null)
            {
                document = await _readRepository.ReadByLaboratoryIdForDocumentMenu(query.Id, query.LaboratoryId.GetValueOrDefault(), cancellationToken);
            }
            else if (query.BioHubFacilityId != null)
            {
                document = await _readRepository.ReadByLaboratoryIdForDocumentMenu(query.Id, query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);
            }
            else
            {
                document = await _readRepository.ReadForDocumentMenu(query.Id, cancellationToken);
            }

            if (document == null)
                return new(new Errors(ErrorType.NotFound, $"Document with Id {query.Id} not found"));

            string instituteId = document.LaboratoryId != null ? document.LaboratoryId.ToString() : document.BioHubFacilityId.ToString();


            string path = string.Empty;

            switch (document.Type)
            {
                case DocumentFileType.SMTA1:
                case DocumentFileType.SMTA2:
                    path = instituteId + "/SMTA/" + document.Type.ToString();
                    break;

                default:
                    string shipmentDirection = document.WorklistFromBioHubItemDocuments.Any() ? "From BioHub" : "Into BioHub";
                    var referenceNumber = document.WorklistFromBioHubItemDocuments.Any() ? document.WorklistFromBioHubItemDocuments.Select(x => x.WorklistFromBioHubItem).FirstOrDefault()?.ReferenceNumber : document.WorklistToBioHubItemDocuments.Select(x => x.WorklistToBioHubItem).FirstOrDefault()?.ReferenceNumber;
                    path = $"{instituteId}/Shipments/{shipmentDirection}/{referenceNumber}/{document.Type.ToString()}";
                    break;
            }

            var fileId = path + "/" + document.Id.ToString() + "." + document.Extension;
            var downloadedFile = await _storageAccountUtility.DownloadDocument(fileId, document.Name);

            if (downloadedFile.IsRight)
            {
                return new(new Errors(ErrorType.NotFound, $"File for DocumentTemplate with Id {query.Id} not found"));
            }

            return new(new ReadDocumentQueryResponse(DownloadedFile: downloadedFile?.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Document with Id {id}", query.Id);
            throw;
        }
    }
}