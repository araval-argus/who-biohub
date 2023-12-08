using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialClinicalDetailReadRepository : IMaterialClinicalDetailReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLMaterialClinicalDetailReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MaterialClinicalDetail>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialClinicalDetails
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<MaterialClinicalDetail> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialClinicalDetails
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}