using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByName;

public interface ISearchLaboratoriesByNameHandler
{
    Task<Either<SearchLaboratoriesByNameQueryResponse, Errors>> Handle(SearchLaboratoriesByNameQuery query, CancellationToken cancellationToken);
}

public class SearchLaboratoriesByNameHandler : ISearchLaboratoriesByNameHandler
{
    private readonly ILogger<SearchLaboratoriesByNameHandler> _logger;
    private readonly SearchLaboratoriesByNameQueryValidator _validator;
    private readonly ILaboratorySearchRepository _readRepository;

    public SearchLaboratoriesByNameHandler(
        ILogger<SearchLaboratoriesByNameHandler> logger,
        SearchLaboratoriesByNameQueryValidator validator,
        ILaboratorySearchRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<SearchLaboratoriesByNameQueryResponse, Errors>> Handle(
        SearchLaboratoriesByNameQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SearchLaboratoriesByNameDALQuery dalQuery = MapQuery(query);

            IEnumerable<Laboratory> laboratories =
                await _readRepository.SearchLaboratoriesByName(dalQuery, cancellationToken);

            return new(new SearchLaboratoriesByNameQueryResponse(laboratories));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error searching Laboratory with query {query}", query);
            throw;
        }
    }

    public SearchLaboratoriesByNameDALQuery MapQuery(SearchLaboratoriesByNameQuery query)
    {
        return new(query.Name);
    }
}