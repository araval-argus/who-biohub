using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialUsagePermissionWriteRepository : IMaterialUsagePermissionWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<MaterialUsagePermission> MaterialUsagePermissions => _dbContext.MaterialUsagePermissions;

    public SQLMaterialUsagePermissionWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<MaterialUsagePermission, Errors>> Create(MaterialUsagePermission materialusagepermission, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(materialusagepermission, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(materialusagepermission);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        MaterialUsagePermission lab = await MaterialUsagePermissions.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        MaterialUsagePermissions.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<MaterialUsagePermission> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialUsagePermissions
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(MaterialUsagePermission materialusagepermission, CancellationToken cancellationToken)
    {
        MaterialUsagePermissions.Update(materialusagepermission);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}