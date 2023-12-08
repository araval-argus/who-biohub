using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;

public interface IUpdateTemperatureUnitOfMeasureMapper
{
    TemperatureUnitOfMeasure Map(TemperatureUnitOfMeasure temperatureunitofmeasure, UpdateTemperatureUnitOfMeasureCommand command);
}

public class UpdateTemperatureUnitOfMeasureMapper : IUpdateTemperatureUnitOfMeasureMapper
{
    public TemperatureUnitOfMeasure Map(TemperatureUnitOfMeasure temperatureunitofmeasure, UpdateTemperatureUnitOfMeasureCommand command)
    {

        temperatureunitofmeasure.Id = command.Id;
        temperatureunitofmeasure.Name = command.Name;
        temperatureunitofmeasure.Unit = command.Unit;
        return temperatureunitofmeasure;
    }
}