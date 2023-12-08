using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Roles;

public interface IRoleReadRepository
{
    Task<Role> Read(Guid id, bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<IEnumerable<Role>> List(bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<Role> ReadWithPermissions(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Role>> GetRolesByPermissionName(string permissionName, CancellationToken cancellationToken);
}
