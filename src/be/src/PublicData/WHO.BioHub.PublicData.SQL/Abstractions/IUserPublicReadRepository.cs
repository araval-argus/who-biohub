using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.SQL.Abstractions
{
    public interface IUserPublicReadRepository
    {
        Task<IEnumerable<User>> ListUsersForRequestAccessEmail(CancellationToken cancellationToken);
    }
}