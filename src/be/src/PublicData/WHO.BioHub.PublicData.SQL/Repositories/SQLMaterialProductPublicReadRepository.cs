using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLMaterialProductPublicReadRepository : IMaterialProductPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialProductPublicReadRepository(BioHubDbContext dbContext)
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