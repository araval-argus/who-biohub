using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;

namespace WHO.BioHub.DAL.Repositories;

public class SQLResourceReadRepository : IResourceReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLResourceReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Resource>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Resources
            .Include(l => l.UploadedBy)
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Resource>> List(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Resources
            .Include(x => x.UploadedBy)
            .Where(l => l.DeletedOn == null && l.ParentId == id)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Resource> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Resources
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}