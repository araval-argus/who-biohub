using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialUsagePermissionReadRepository : IMaterialUsagePermissionReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialUsagePermissionReadRepository(BioHubDbContext dbContext)
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