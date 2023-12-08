using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBSLLevelReadRepository : IBSLLevelReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLBSLLevelReadRepository(BioHubDbContext dbContext)
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