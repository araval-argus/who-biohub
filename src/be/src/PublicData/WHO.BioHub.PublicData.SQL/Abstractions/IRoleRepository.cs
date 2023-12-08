using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IRolePublicReadRepository
{
    Task<Role> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Role>> List(CancellationToken cancellationToken);
}
