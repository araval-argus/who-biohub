using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IMaterialUsagePermissionPublicReadRepository
{
    Task<MaterialUsagePermission> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialUsagePermission>> List(CancellationToken cancellationToken);
}
