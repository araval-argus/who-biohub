using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLBSLLevelPublicReadRepository : IBSLLevelPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLBSLLevelPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BSLLevel>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.BSLLevels
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<BSLLevel> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BSLLevels
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}