using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLMaterialTypeWriteRepository : IMaterialTypeWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<MaterialType> MaterialTypes => _dbContext.MaterialTypes;

    public SQLMaterialTypeWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<MaterialType, Errors>> Create(MaterialType materialtype, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(materialtype, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(materialtype);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        MaterialType lab = await MaterialTypes.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        MaterialTypes.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<MaterialType> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MaterialTypes
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(MaterialType materialtype, CancellationToken cancellationToken)
    {
        MaterialTypes.Update(materialtype);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}