namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;

public record struct CreateTemperatureUnitOfMeasureCommand(
    string Name,
    string Unit
);