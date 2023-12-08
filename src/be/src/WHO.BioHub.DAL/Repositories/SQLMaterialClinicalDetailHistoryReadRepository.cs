using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialClinicalDetailHistoryReadRepository : IMaterialClinicalDetailHistoryReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialClinicalDetailHistoryReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MaterialClinicalDetailHistory>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialClinicalDetailsHistory
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialClinicalDetailHistory> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialClinicalDetailsHistory
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}