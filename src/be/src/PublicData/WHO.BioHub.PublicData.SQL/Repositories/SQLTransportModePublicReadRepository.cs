using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportModes;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLTransportModePublicReadRepository : ITransportModePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLTransportModePublicReadRepository(BioHubDbContext dbContext)
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