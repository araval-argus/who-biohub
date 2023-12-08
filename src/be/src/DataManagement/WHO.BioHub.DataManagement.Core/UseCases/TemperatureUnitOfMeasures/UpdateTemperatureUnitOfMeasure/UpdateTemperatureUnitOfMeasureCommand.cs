namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;

public record struct UpdateTemperatureUnitOfMeasureCommand(Guid Id,
    string Name,
    string Unit);