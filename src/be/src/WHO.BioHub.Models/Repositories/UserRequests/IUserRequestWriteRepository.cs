using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.UserRequests;

public interface IUserRequestWriteRepository
{
    Task<Either<UserRequest, Errors>> Create(UserRequest userRequest, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<UserRequest> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(UserRequest userRequest, CancellationToken cancellationToken);
}
