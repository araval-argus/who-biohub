using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ReadTemperatureUnitOfMeasure;

public interface IReadTemperatureUnitOfMeasureHandler
{
    Task<Either<ReadTemperatureUnitOfMeasureQueryResponse, Errors>> Handle(ReadTemperatureUnitOfMeasureQuery query, CancellationToken cancellationToken);
}

public class ReadTemperatureUnitOfMeasureHandler : IReadTemperatureUnitOfMeasureHandler
{
    private readonly ILogger<ReadTemperatureUnitOfMeasureHandler> _logger;
    private readonly ReadTemperatureUnitOfMeasureQueryValidator _validator;
    private readonly ITemperatureUnitOfMeasureReadRepository _readRepository;

    public ReadTemperatureUnitOfMeasureHandler(
        ILogger<ReadTemperatureUnitOfMeasureHandler> logger,
        ReadTemperatureUnitOfMeasureQueryValidator validator,
        ITemperatureUnitOfMeasureReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ReadTemperatureUnitOfMeasureQueryResponse, Errors>> Handle(
        ReadTemperatureUnitOfMeasureQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            TemperatureUnitOfMeasure temperatureunitofmeasure = await _readRepository.Read(query.Id, cancellationToken);
            if (temperatureunitofmeasure == null)
                return new(new Errors(ErrorType.NotFound, $"TemperatureUnitOfMeasure with Id {query.Id} not found"));

            return new(new ReadTemperatureUnitOfMeasureQueryResponse(temperatureunitofmeasure));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading TemperatureUnitOfMeasure with Id {id}", query.Id);
            throw;
        }
    }
}