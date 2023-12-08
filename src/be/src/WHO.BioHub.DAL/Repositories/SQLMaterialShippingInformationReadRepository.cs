using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialShippingInformationReadRepository : IMaterialShippingInformationReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialShippingInformationReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MaterialShippingInformation>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialShippingInformations
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialShippingInformation> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialShippingInformations
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}