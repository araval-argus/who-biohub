using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLMaterialTypePublicReadRepository : IMaterialTypePublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialTypePublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MaterialType>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialTypes
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialType> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}