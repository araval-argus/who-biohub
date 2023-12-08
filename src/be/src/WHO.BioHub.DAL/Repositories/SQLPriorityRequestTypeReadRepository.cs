using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;

namespace WHO.BioHub.DAL.Repositories;

public class SQLPriorityRequestTypeReadRepository : IPriorityRequestTypeReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLPriorityRequestTypeReadRepository(BioHubDbContext dbContext)
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