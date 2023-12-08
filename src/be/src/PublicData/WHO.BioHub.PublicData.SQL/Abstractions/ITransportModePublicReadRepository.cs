using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface ITransportModePublicReadRepository
{
    Task<TransportMode> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TransportMode>> List(CancellationToken cancellationToken);
}
