using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.Countries.ReadCountry;

public interface IReadCountryHandler
{
    Task<Either<ReadCountryQueryResponse, Errors>> Handle(ReadCountryQuery query, CancellationToken cancellationToken);
}

public class ReadCountryHandler : IReadCountryHandler
{
    private readonly ILogger<ReadCountryHandler> _logger;
    private readonly ReadCountryQueryValidator _validator;
    private readonly ICountryPublicReadRepository _readRepository;

    public ReadCountryHandler(
        ILogger<ReadCountryHandler> logger,
        ReadCountryQueryValidator validator,
        ICountryPublicReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadCountryQueryResponse, Errors>> Handle(
        ReadCountryQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Country country = await _readRepository.Read(query.Id, cancellationToken);
            if (country == null)
                return new(new Errors(ErrorType.NotFound, $"Country with Id {query.Id} not found"));

            return new(new ReadCountryQueryResponse(country));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading Country with Id {id}", query.Id);
            throw;
        }
    }
}