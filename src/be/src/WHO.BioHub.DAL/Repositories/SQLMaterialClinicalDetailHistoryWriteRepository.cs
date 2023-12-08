using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialClinicalDetailHistoryWriteRepository : IMaterialClinicalDetailHistoryWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<MaterialClinicalDetailHistory> MaterialClinicalDetailsHistory => _dbContext.MaterialClinicalDetailsHistory;

    public SQLMaterialClinicalDetailHistoryWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<MaterialClinicalDetailHistory, Errors>> Create(MaterialClinicalDetailHistory materialclinicaldetailhistory, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(materialclinicaldetailhistory, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(materialclinicaldetailhistory);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        MaterialClinicalDetailHistory lab = await MaterialClinicalDetailsHistory.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        MaterialClinicalDetailsHistory.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<MaterialClinicalDetailHistory> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialClinicalDetailsHistory
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(MaterialClinicalDetailHistory materialclinicaldetailhistory, CancellationToken cancellationToken)
    {
        MaterialClinicalDetailsHistory.Update(materialclinicaldetailhistory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}