using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLTransportModeWriteRepository : ITransportModeWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<TransportMode> TransportModes => _dbContext.TransportModes;

    public SQLTransportModeWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<TransportMode, Errors>> Create(TransportMode transportmode, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(transportmode, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(transportmode);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        TransportMode lab = await TransportModes.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        TransportModes.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<TransportMode> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.TransportModes
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(TransportMode transportmode, CancellationToken cancellationToken)
    {
        TransportModes.Update(transportmode);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}