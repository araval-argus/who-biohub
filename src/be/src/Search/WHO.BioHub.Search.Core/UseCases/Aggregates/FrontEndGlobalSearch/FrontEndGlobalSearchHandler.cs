using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Search.Core.Repositories.Aggregates;

using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.Core.UseCases.Aggregates.FrontEndGlobalSearch;

public interface IFrontEndGlobalSearchHandler
{
    Task<Either<FrontEndGlobalSearchQueryResponse, Errors>> Handle(FrontEndGlobalSearchQuery query, CancellationToken cancellationToken);
}

public class FrontEndGlobalSearchHandler : IFrontEndGlobalSearchHandler
{
    private readonly ILogger<FrontEndGlobalSearchHandler> _logger;
    private readonly FrontEndGlobalSearchQueryValidator _validator;
    private readonly IFrontEndGlobalSearchRepository _readRepository;

    public FrontEndGlobalSearchHandler(
        ILogger<FrontEndGlobalSearchHandler> logger,
        FrontEndGlobalSearchQueryValidator validator,
        IFrontEndGlobalSearchRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<FrontEndGlobalSearchQueryResponse, Errors>> Handle(
        FrontEndGlobalSearchQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            FrontEndGlobalSearchDALQuery dalQuery = MapQuery(query);

            FrontEndGlobalSearchDALResponse frontendglobalsearch =
                await _readRepository.FrontEndGlobalSearch(dalQuery, cancellationToken);

            FrontEndGlobalSearchQueryResponse response = MapResponse(frontendglobalsearch);
            return new(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error searching FrontEndGlobalSearch with query {query}", query);
            throw;
        }
    }

    public static FrontEndGlobalSearchDALQuery MapQuery(FrontEndGlobalSearchQuery query)
    {
        return new(query.LaboratoryName);
    }

    public static FrontEndGlobalSearchQueryResponse MapResponse(FrontEndGlobalSearchDALResponse dalResponse)
    {
        return new(dalResponse.Laboratories);
    }
}