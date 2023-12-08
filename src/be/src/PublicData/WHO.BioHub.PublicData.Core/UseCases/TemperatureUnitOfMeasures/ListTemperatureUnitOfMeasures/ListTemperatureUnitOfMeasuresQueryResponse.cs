using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;

public record struct ListTemperatureUnitOfMeasuresQueryResponse(IEnumerable<TemperatureUnitOfMeasurePublicDto> TemperatureUnitOfMeasures) { }