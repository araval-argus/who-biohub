using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;

public interface IListTemperatureUnitOfMeasuresHandler
{
    Task<Either<ListTemperatureUnitOfMeasuresQueryResponse, Errors>> Handle(ListTemperatureUnitOfMeasuresQuery query, CancellationToken cancellationToken);
}

public class ListTemperatureUnitOfMeasuresHandler : IListTemperatureUnitOfMeasuresHandler
{
    private readonly ILogger<ListTemperatureUnitOfMeasuresHandler> _logger;
    private readonly ListTemperatureUnitOfMeasuresQueryValidator _validator;
    private readonly ITemperatureUnitOfMeasureReadRepository _readRepository;

    public ListTemperatureUnitOfMeasuresHandler(
        ILogger<ListTemperatureUnitOfMeasuresHandler> logger,
        ListTemperatureUnitOfMeasuresQueryValidator validator,
        ITemperatureUnitOfMeasureReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListTemperatureUnitOfMeasuresQueryResponse, Errors>> Handle(
        ListTemperatureUnitOfMeasuresQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<TemperatureUnitOfMeasure> temperatureunitofmeasures = await _readRepository.List(cancellationToken);
            var temperatureUnitOfMeasureDtos = new List<TemperatureUnitOfMeasureDto>();
            foreach (var temperatureUnitOfMeasure in temperatureunitofmeasures)
            {
                TemperatureUnitOfMeasureDto temperatureUnitOfMeasureDto = new()
                {
                    Id = temperatureUnitOfMeasure.Id,
                    Name = temperatureUnitOfMeasure.Name,
                    Unit = temperatureUnitOfMeasure.Unit,
                };

                temperatureUnitOfMeasureDtos.Add(temperatureUnitOfMeasureDto);
            }

            return new(new ListTemperatureUnitOfMeasuresQueryResponse(temperatureUnitOfMeasureDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all TemperatureUnitOfMeasures");
            throw;
        }
    }
}