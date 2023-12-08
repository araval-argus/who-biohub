using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialShippingInformationHistoryWriteRepository : IMaterialShippingInformationHistoryWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<MaterialShippingInformationHistory> MaterialShippingInformationsHistory => _dbContext.MaterialShippingInformationsHistory;

    public SQLMaterialShippingInformationHistoryWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<MaterialShippingInformationHistory, Errors>> Create(MaterialShippingInformationHistory materialshippinginformationhistory, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(materialshippinginformationhistory, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(materialshippinginformationhistory);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        MaterialShippingInformationHistory lab = await MaterialShippingInformationsHistory.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        MaterialShippingInformationsHistory.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<MaterialShippingInformationHistory> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialShippingInformationsHistory
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(MaterialShippingInformationHistory materialshippinginformationhistory, CancellationToken cancellationToken)
    {
        MaterialShippingInformationsHistory.Update(materialshippinginformationhistory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}