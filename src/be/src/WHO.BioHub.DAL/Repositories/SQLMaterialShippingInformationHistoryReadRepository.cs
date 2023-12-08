using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialShippingInformationHistoryReadRepository : IMaterialShippingInformationHistoryReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialShippingInformationHistoryReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MaterialShippingInformationHistory>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialShippingInformationsHistory
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialShippingInformationHistory> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialShippingInformationsHistory
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}