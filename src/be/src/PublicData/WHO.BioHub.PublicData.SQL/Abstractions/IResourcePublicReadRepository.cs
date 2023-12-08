using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IResourcePublicReadRepository
{
    Task<Resource> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Resource>> List(CancellationToken cancellationToken);
    Task<IEnumerable<Resource>> List(Guid id, CancellationToken cancellationToken);
}
