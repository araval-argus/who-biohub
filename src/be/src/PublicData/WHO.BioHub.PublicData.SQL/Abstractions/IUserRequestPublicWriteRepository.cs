using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IUserRequestPublicWriteRepository
{
    Task<Either<UserRequest, Errors>> Create(UserRequest userRequest, CancellationToken cancellationToken);
    Task<UserRequest> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(UserRequest userRequest, CancellationToken cancellationToken);
}
