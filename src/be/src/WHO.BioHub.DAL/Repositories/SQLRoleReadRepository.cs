using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;

namespace WHO.BioHub.DAL.Repositories;

public class SQLRoleReadRepository : IRoleReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLRoleReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Role>> List(bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Roles
            .Where(l => l.DeletedOn == null);

        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.OnBehalfOf != true);
        }

        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<Role> Read(Guid id, bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Roles
           .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.Id == id);

        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.OnBehalfOf != true);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Role> ReadWithPermissions(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Roles
            .Include(x => x.RolePermissions)
            .ThenInclude(x => x.Permission)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id);

        
    }

    public async Task<IEnumerable<Role>> GetRolesByPermissionName(string permissionName, CancellationToken cancellationToken)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.RolePermissions.Select(x => x.Permission).Select(x => x.Name).Contains(permissionName))
            .ToArrayAsync(cancellationToken);
    }
}