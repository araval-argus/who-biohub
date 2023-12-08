using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLTemperatureUnitOfMeasurePublicReadRepository : ITemperatureUnitOfMeasurePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLTemperatureUnitOfMeasurePublicReadRepository(BioHubDbContext dbContext)
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