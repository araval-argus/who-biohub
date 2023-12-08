using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;

namespace WHO.BioHub.DAL.Repositories;

public class SQLTemperatureUnitOfMeasureReadRepository : ITemperatureUnitOfMeasureReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLTemperatureUnitOfMeasureReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TemperatureUnitOfMeasure>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.TemperatureUnitOfMeasures
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<TemperatureUnitOfMeasure> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.TemperatureUnitOfMeasures
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}