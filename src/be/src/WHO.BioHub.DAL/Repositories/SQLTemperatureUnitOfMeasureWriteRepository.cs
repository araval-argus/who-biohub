using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TemperatureUnitOfMeasures;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLTemperatureUnitOfMeasureWriteRepository : ITemperatureUnitOfMeasureWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<TemperatureUnitOfMeasure> TemperatureUnitOfMeasures => _dbContext.TemperatureUnitOfMeasures;

    public SQLTemperatureUnitOfMeasureWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<TemperatureUnitOfMeasure, Errors>> Create(TemperatureUnitOfMeasure temperatureunitofmeasure, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(temperatureunitofmeasure, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(temperatureunitofmeasure);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        TemperatureUnitOfMeasure lab = await TemperatureUnitOfMeasures.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        TemperatureUnitOfMeasures.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<TemperatureUnitOfMeasure> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.TemperatureUnitOfMeasures
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(TemperatureUnitOfMeasure temperatureunitofmeasure, CancellationToken cancellationToken)
    {
        TemperatureUnitOfMeasures.Update(temperatureunitofmeasure);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}