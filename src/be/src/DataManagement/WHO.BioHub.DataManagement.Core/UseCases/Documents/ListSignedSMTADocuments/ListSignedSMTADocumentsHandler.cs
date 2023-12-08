using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocument;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocuments;

public interface IListSignedSMTADocumentsHandler
{
    Task<Either<ListSignedSMTADocumentsQueryResponse, Errors>> Handle(ListSignedSMTADocumentsQuery query, CancellationToken cancellationToken);
}

public class ListSignedSMTADocumentsHandler : IListSignedSMTADocumentsHandler
{
    private readonly ILogger<ListSignedSMTADocumentsHandler> _logger;
    private readonly ListSignedSMTADocumentsQueryValidator _validator;
    private readonly IDocumentReadRepository _readRepository;
    private readonly IListSignedSMTADocumentsMapper _listDocumentsMapper;

    public ListSignedSMTADocumentsHandler(
        ILogger<ListSignedSMTADocumentsHandler> logger,
        ListSignedSMTADocumentsQueryValidator validator,
        IDocumentReadRepository readRepository,
        IListSignedSMTADocumentsMapper listDocumentsMapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _listDocumentsMapper = listDocumentsMapper;
    }

    public async Task<Either<ListSignedSMTADocumentsQueryResponse, Errors>> Handle(
        ListSignedSMTADocumentsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        IEnumerable<Document> documents;

        try
        {

            documents = await _readRepository.ListSignedSMTA(query.LaboratoryId, query.BioHubFacilityId, cancellationToken);

            var documentsDto = _listDocumentsMapper.Map(documents.ToList());

            return new(new ListSignedSMTADocumentsQueryResponse(documentsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Documents");
            throw;
        }
    }
}