using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialUsagePermissions;

public interface IMaterialUsagePermissionWriteRepository
{
    Task<Either<MaterialUsagePermission, Errors>> Create(MaterialUsagePermission materialusagepermission, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<MaterialUsagePermission> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(MaterialUsagePermission materialusagepermission, CancellationToken cancellationToken);
}
