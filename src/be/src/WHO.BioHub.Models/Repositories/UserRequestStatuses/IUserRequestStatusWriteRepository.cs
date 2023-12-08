using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.UserRequestStatuses;

public interface IUserRequestStatusWriteRepository
{
    Task<Either<UserRequestStatus, Errors>> Create(UserRequestStatus userRequestStatus, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<UserRequestStatus> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(UserRequestStatus userRequestStatus, CancellationToken cancellationToken);
}
