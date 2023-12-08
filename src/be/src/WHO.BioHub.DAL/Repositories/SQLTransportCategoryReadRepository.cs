using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;

namespace WHO.BioHub.DAL.Repositories;

public class SQLTransportCategoryReadRepository : ITransportCategoryReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLTransportCategoryReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TransportCategory>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.TransportCategories
            .Where(l => l.DeletedOn == null)
            .OrderBy(l => l.Name)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<TransportCategory> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.TransportCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}