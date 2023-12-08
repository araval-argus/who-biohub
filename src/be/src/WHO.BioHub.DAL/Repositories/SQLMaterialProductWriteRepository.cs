using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialProducts;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialProductWriteRepository : IMaterialProductWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<MaterialProduct> MaterialProducts => _dbContext.MaterialProducts;

    public SQLMaterialProductWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<MaterialProduct, Errors>> Create(MaterialProduct materialproduct, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(materialproduct, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(materialproduct);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        MaterialProduct lab = await MaterialProducts.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        MaterialProducts.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<MaterialProduct> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialProducts
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(MaterialProduct materialproduct, CancellationToken cancellationToken)
    {
        MaterialProducts.Update(materialproduct);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}