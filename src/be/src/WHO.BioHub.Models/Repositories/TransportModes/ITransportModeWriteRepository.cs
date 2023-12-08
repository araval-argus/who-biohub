using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.TransportModes;

public interface ITransportModeWriteRepository
{
    Task<Either<TransportMode, Errors>> Create(TransportMode transportmode, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<TransportMode> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(TransportMode transportmode, CancellationToken cancellationToken);
}
