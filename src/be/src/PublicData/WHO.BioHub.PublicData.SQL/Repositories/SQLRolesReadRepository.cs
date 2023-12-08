using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLRolePublicReadRepository : IRolePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLRolePublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Role>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Roles
            .Where(l => l.DeletedOn == null)
            .Where(l => l.AddToRegistration == true)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Role> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.AddToRegistration == true, cancellationToken);
    }
}