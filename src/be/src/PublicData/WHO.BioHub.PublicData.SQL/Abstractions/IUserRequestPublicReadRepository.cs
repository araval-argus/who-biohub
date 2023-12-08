using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IUserRequestPublicReadRepository
{
    Task<UserRequest> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<UserRequest>> List(CancellationToken cancellationToken);
    Task<bool> EmailPresent(string email, CancellationToken cancellationToken);
}
