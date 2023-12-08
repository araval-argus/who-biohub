using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLCultivabilityTypePublicReadRepository : ICultivabilityTypePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLCultivabilityTypePublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CultivabilityType>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.CultivabilityTypes
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<CultivabilityType> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.CultivabilityTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}