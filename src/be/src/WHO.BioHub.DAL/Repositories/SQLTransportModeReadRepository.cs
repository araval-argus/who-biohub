using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;

namespace WHO.BioHub.DAL.Repositories;

public class SQLTransportModeReadRepository : ITransportModeReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLTransportModeReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TransportMode>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.TransportModes
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<TransportMode> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.TransportModes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}