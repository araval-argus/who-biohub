using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;

public record struct ListTemperatureUnitOfMeasuresQueryResponse(IEnumerable<TemperatureUnitOfMeasureDto> TemperatureUnitOfMeasures) { }