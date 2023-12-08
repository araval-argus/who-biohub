using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialUsagePermissions;

public interface IMaterialUsagePermissionReadRepository
{
    Task<MaterialUsagePermission> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialUsagePermission>> List(CancellationToken cancellationToken);
}
