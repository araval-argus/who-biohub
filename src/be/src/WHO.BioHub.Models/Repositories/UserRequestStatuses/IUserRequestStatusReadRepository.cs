using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.UserRequestStatuses;

public interface IUserRequestStatusReadRepository
{
    Task<UserRequestStatus> Read(Guid id, CancellationToken cancellationToken);
    Task<UserRequestStatus> ReadByStatus(UserRegistrationStatus status, CancellationToken cancellationToken);
    Task<IEnumerable<UserRequestStatus>> List(CancellationToken cancellationToken);
}
