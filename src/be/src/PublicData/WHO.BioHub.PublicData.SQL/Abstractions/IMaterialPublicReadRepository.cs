using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IMaterialPublicReadRepository
{
    Task<Material> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> List(CancellationToken cancellationToken);
}
