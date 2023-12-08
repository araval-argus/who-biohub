using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using System.Linq;
using WHO.BioHub.PublicData.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLUserPublicReadRepository : IUserPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLUserPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> ListUsersForRequestAccessEmail(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Where(l => l.DeletedOn == null)
            .Where(l => l.Role.RolePermissions.Select(x => x.Permission.Name).Contains(PermissionNames.CanReceiveEmailsOnRequestAccess))
            .ToArrayAsync(cancellationToken);
    }
}