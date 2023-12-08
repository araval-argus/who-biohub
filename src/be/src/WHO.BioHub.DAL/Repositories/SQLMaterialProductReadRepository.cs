using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialProductReadRepository : IMaterialProductReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialProductReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MaterialProduct>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialProducts
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialProduct> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialProducts
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}