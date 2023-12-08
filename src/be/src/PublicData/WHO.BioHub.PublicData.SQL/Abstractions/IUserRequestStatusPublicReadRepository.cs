using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IUserRequestStatusPublicReadRepository
{
    Task<UserRequestStatus> Read(Guid id, CancellationToken cancellationToken);
    Task<UserRequestStatus> ReadByStatus(UserRegistrationStatus status, CancellationToken cancellationToken);
}
