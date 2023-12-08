using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;

namespace WHO.BioHub.DAL.Repositories;

public class SQLIsolationTechniqueTypeReadRepository : IIsolationTechniqueTypeReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLIsolationTechniqueTypeReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<IsolationTechniqueType>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.IsolationTechniqueTypes
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IsolationTechniqueType> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.IsolationTechniqueTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}