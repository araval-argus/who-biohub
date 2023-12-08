using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLResourcePublicReadRepository : IResourcePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLResourcePublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Resource>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Resources
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<Resource>> List(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Resources            
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