using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLResourceWriteRepository : IResourceWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Resource> Resources => _dbContext.Resources;

    public SQLResourceWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Resource, Errors>> Create(Resource resource, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(resource, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(resource);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        Resource lab = await Resources.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Resources.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Resource> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Resources
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(Resource resource, CancellationToken cancellationToken)
    {
        Resources.Update(resource);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }

    public async Task<List<Guid>> GetIdsForDelete(IEnumerable<Guid> parentIds, CancellationToken cancellationToken)
    {
        return await _dbContext.Resources
            .Where(l => l.DeletedOn == null)
            .Where(l => l.ParentId != null)
            .Where(l => parentIds.Contains(l.ParentId.Value))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

    }

    public async Task<Errors?> DeleteRange(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        List<Resource> documentTemplates = await Resources.Where(l => l.DeletedOn == null && ids.Contains(l.Id)).ToListAsync(cancellationToken);
        foreach (var resource in documentTemplates)
        {
            resource.DeletedOn = DateTime.UtcNow;
        }

        Resources.UpdateRange(documentTemplates);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }
}