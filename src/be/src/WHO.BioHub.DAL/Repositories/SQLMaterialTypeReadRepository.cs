using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialTypeReadRepository : IMaterialTypeReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialTypeReadRepository(BioHubDbContext dbContext)
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