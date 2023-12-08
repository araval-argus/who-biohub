using EFCore.BulkExtensions;
using WHO.BioHub.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace WHO.BioHub.DAL.Seeder.Data;

internal partial class SeedData
{
    internal static TemperatureUnitOfMeasure[] TemperatureUnitOfMeasures => new TemperatureUnitOfMeasure[]
    {
        new()
        {
            Id = TemperatureUnitOfMeasureId1,
            Name = "Celsius",
            Unit = "°C",
        },
    };

    internal static Guid TemperatureUnitOfMeasureId1 => Guid.Parse("d4316d59-96a0-4e77-9061-4e2e72126e43");

    private async Task AddOrUpdateTemperatureUnitOfMeasures(CancellationToken cancellationToken)
    {        
        foreach (var temperatureUnitOfMeasure in TemperatureUnitOfMeasures)
        {
            if (await _db.TemperatureUnitOfMeasures.Where(x => x.Id == temperatureUnitOfMeasure.Id).AnyAsync(cancellationToken))
            {
                _db.Update(temperatureUnitOfMeasure);
            }
            else
            {
                await _db.AddAsync(temperatureUnitOfMeasure);
            }
        }
    }
}
