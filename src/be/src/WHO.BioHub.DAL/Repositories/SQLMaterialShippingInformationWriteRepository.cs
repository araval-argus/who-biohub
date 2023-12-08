using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialShippingInformationWriteRepository : IMaterialShippingInformationWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<MaterialShippingInformation> MaterialShippingInformations => _dbContext.MaterialShippingInformations;

    public SQLMaterialShippingInformationWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<MaterialShippingInformation, Errors>> Create(MaterialShippingInformation materialshippinginformation, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(materialshippinginformation, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(materialshippinginformation);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        MaterialShippingInformation lab = await MaterialShippingInformations.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        MaterialShippingInformations.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<MaterialShippingInformation> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialShippingInformations
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(MaterialShippingInformation materialshippinginformation, CancellationToken cancellationToken)
    {
        MaterialShippingInformations.Update(materialshippinginformation);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}