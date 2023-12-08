using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;

public interface ITemperatureUnitOfMeasureReadRepository
{
    Task<TemperatureUnitOfMeasure> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TemperatureUnitOfMeasure>> List(CancellationToken cancellationToken);
}
