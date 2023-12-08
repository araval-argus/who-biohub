using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLPriorityRequestTypePublicReadRepository : IPriorityRequestTypePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLPriorityRequestTypePublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PriorityRequestType>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.PriorityRequestTypes
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<PriorityRequestType> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.PriorityRequestTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}