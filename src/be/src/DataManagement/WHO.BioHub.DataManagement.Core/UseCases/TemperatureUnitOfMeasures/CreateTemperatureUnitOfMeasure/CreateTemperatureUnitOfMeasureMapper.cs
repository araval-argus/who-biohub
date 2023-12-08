using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;

public interface ICreateTemperatureUnitOfMeasureMapper
{
    TemperatureUnitOfMeasure Map(CreateTemperatureUnitOfMeasureCommand command);
}

public class CreateTemperatureUnitOfMeasureMapper : ICreateTemperatureUnitOfMeasureMapper
{
    public TemperatureUnitOfMeasure Map(CreateTemperatureUnitOfMeasureCommand command)
    {

        TemperatureUnitOfMeasure temperatureunitofmeasure = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Unit = command.Unit,
            DeletedOn = null,
        };

        return temperatureunitofmeasure;
    }
}