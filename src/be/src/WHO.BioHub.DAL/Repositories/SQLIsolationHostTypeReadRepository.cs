using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;

namespace WHO.BioHub.DAL.Repositories;

public class SQLIsolationHostTypeReadRepository : IIsolationHostTypeReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLIsolationHostTypeReadRepository(BioHubDbContext dbContext)
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