using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByCountry;

public interface ISearchLaboratoriesByCountryHandler
{
    Task<Either<SearchLaboratoriesByCountryQueryResponse, Errors>> Handle(SearchLaboratoriesByCountryQuery query, CancellationToken cancellationToken);
}

public class SearchLaboratoriesByCountryHandler : ISearchLaboratoriesByCountryHandler
{
    private readonly ILogger<SearchLaboratoriesByCountryHandler> _logger;
    private readonly SearchLaboratoriesByCountryQueryValidator _validator;
    private readonly ILaboratorySearchRepository _readRepository;

    public SearchLaboratoriesByCountryHandler(
        ILogger<SearchLaboratoriesByCountryHandler> logger,
        SearchLaboratoriesByCountryQueryValidator validator,
        ILaboratorySearchRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<SearchLaboratoriesByCountryQueryResponse, Errors>> Handle(
        SearchLaboratoriesByCountryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            SearchLaboratoriesByCountryDALQuery dalQuery = MapQuery(query);

            IEnumerable<Laboratory> laboratories =
                await _readRepository.SearchLaboratoriesByCountry(dalQuery, cancellationToken);

            return new(new SearchLaboratoriesByCountryQueryResponse(laboratories));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error searching Laboratory with query {query}", query);
            throw;
        }
    }

    public static SearchLaboratoriesByCountryDALQuery MapQuery(SearchLaboratoriesByCountryQuery query)
    {
        return new(query.Country);
    }
}