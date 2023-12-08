using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Roles;

public interface IRoleWriteRepository
{
    Task<Either<Role, Errors>> Create(Role role, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Role> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Role role, CancellationToken cancellationToken);
}
