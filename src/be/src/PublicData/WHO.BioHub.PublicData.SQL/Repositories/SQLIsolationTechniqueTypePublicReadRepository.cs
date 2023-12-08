using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLIsolationTechniqueTypePublicReadRepository : IIsolationTechniqueTypePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLIsolationTechniqueTypePublicReadRepository(BioHubDbContext dbContext)
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