using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;

namespace WHO.BioHub.DAL.Repositories;

public class SQLCultivabilityTypeReadRepository : ICultivabilityTypeReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLCultivabilityTypeReadRepository(BioHubDbContext dbContext)
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