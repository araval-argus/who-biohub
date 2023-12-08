using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface ITransportCategoryPublicReadRepository
{
    Task<TransportCategory> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TransportCategory>> List(CancellationToken cancellationToken);
}
