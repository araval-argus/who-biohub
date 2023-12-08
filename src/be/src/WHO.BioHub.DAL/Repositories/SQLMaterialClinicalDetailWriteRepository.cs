using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialClinicalDetails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialClinicalDetailWriteRepository : IMaterialClinicalDetailWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<MaterialClinicalDetail> MaterialClinicalDetails => _dbContext.MaterialClinicalDetails;

    public SQLMaterialClinicalDetailWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<MaterialClinicalDetail, Errors>> Create(MaterialClinicalDetail materialclinicaldetail, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(materialclinicaldetail, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(materialclinicaldetail);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        MaterialClinicalDetail lab = await MaterialClinicalDetails.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        MaterialClinicalDetails.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<MaterialClinicalDetail> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialClinicalDetails
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(MaterialClinicalDetail materialclinicaldetail, CancellationToken cancellationToken)
    {
        MaterialClinicalDetails.Update(materialclinicaldetail);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}