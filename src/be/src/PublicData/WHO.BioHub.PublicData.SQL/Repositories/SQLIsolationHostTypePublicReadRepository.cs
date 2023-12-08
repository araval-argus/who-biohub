using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLIsolationHostTypePublicReadRepository : IIsolationHostTypePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLIsolationHostTypePublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<IsolationHostType>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.IsolationHostTypes
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IsolationHostType> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.IsolationHostTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}