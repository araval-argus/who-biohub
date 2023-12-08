using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocument;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocuments;

public interface IListDocumentsHandler
{
    Task<Either<ListDocumentsQueryResponse, Errors>> Handle(ListDocumentsQuery query, CancellationToken cancellationToken);
}

public class ListDocumentsHandler : IListDocumentsHandler
{
    private readonly ILogger<ListDocumentsHandler> _logger;
    private readonly ListDocumentsQueryValidator _validator;
    private readonly IDocumentReadRepository _readRepository;
    private readonly IListDocumentsMapper _listDocumentsMapper;
    private readonly IWorklistToBioHubItemReadRepository _readToBioHubRepository;
    private readonly IWorklistFromBioHubItemReadRepository _readFromBioHubRepository;

    public ListDocumentsHandler(
        ILogger<ListDocumentsHandler> logger,
        ListDocumentsQueryValidator validator,
        IDocumentReadRepository readRepository,
        IListDocumentsMapper listDocumentsMapper,
        IWorklistToBioHubItemReadRepository readToBioHubRepository,
        IWorklistFromBioHubItemReadRepository readFromBioHubRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _listDocumentsMapper = listDocumentsMapper;
        _readToBioHubRepository = readToBioHubRepository;
        _readFromBioHubRepository = readFromBioHubRepository;
    }

    public async Task<Either<ListDocumentsQueryResponse, Errors>> Handle(
        ListDocumentsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IEnumerable<Document> documents;

        try
        {
            if (query.LaboratoryId != null)
            {
                documents = await _readRepository.ListByLaboratoryId(query.LaboratoryId.GetValueOrDefault(), cancellationToken);
            }
            else if (query.BioHubFacilityId != null)
            {
                documents = await _readRepository.ListByBioHubFacilityId(query.BioHubFacilityId.GetValueOrDefault(), cancellationToken);
            }
            else
            {
                documents = await _readRepository.List(cancellationToken);
            }

            var worklistToBioHubItemIds = documents.Select(x => x.WorklistToBioHubItemDocuments.Select(x => x.WorklistToBioHubItemId.GetValueOrDefault())).SelectMany(x => x);
            var worklistFromBioHubItemIds = documents.Select(x => x.WorklistFromBioHubItemDocuments.Select(x => x.WorklistFromBioHubItemId.GetValueOrDefault())).SelectMany(x => x);

            List<WorklistToBioHubItem> worklistToBioHubItems;
            List<WorklistFromBioHubItem> worklistFromBioHubItems;

            if (worklistToBioHubItemIds != null && worklistToBioHubItemIds.Any())
            {
                worklistToBioHubItems = (await _readToBioHubRepository.ListByIds(worklistToBioHubItemIds.ToList(), cancellationToken)).ToList();
            }
            else
            {
                worklistToBioHubItems = new List<WorklistToBioHubItem>();
            }
            if (worklistFromBioHubItemIds != null && worklistFromBioHubItemIds.Any())
            {
                worklistFromBioHubItems = (await _readFromBioHubRepository.ListByIds(worklistFromBioHubItemIds.ToList(), cancellationToken)).ToList();
            }
            else
            {
                worklistFromBioHubItems = new List<WorklistFromBioHubItem>();
            }
            var documentsDto = _listDocumentsMapper.Map(documents.ToList(), worklistToBioHubItems, worklistFromBioHubItems);

            return new(new ListDocumentsQueryResponse(documentsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Documents");
            throw;
        }
    }
}