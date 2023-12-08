using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.UserRequests;

public interface IUserRequestReadRepository
{
    Task<UserRequest> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<UserRequest>> List(CancellationToken cancellationToken);
}
