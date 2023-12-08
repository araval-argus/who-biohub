using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface ITemperatureUnitOfMeasurePublicReadRepository
{
    Task<TemperatureUnitOfMeasure> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TemperatureUnitOfMeasure>> List(CancellationToken cancellationToken);
}
