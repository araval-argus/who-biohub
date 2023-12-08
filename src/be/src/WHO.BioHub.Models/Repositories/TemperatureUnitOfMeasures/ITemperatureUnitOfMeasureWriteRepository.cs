using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;

public interface ITemperatureUnitOfMeasureWriteRepository
{
    Task<Either<TemperatureUnitOfMeasure, Errors>> Create(TemperatureUnitOfMeasure temperatureunitofmeasure, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<TemperatureUnitOfMeasure> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(TemperatureUnitOfMeasure temperatureunitofmeasure, CancellationToken cancellationToken);
}
