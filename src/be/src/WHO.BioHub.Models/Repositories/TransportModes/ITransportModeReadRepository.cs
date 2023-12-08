using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.TransportModes;

public interface ITransportModeReadRepository
{
    Task<TransportMode> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TransportMode>> List(CancellationToken cancellationToken);
}
