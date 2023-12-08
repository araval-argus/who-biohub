using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLMaterialUsagePermissionPublicReadRepository : IMaterialUsagePermissionPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialUsagePermissionPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MaterialUsagePermission>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialUsagePermissions
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialUsagePermission> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialUsagePermissions
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}