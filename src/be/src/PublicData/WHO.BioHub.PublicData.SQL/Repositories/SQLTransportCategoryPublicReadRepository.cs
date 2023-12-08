using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLTransportCategoryPublicReadRepository : ITransportCategoryPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLTransportCategoryPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TransportCategory>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.TransportCategories
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<TransportCategory> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.TransportCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}