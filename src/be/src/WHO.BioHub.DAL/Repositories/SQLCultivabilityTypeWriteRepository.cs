using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.CultivabilityTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLCultivabilityTypeWriteRepository : ICultivabilityTypeWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<CultivabilityType> CultivabilityTypes => _dbContext.CultivabilityTypes;

    public SQLCultivabilityTypeWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<CultivabilityType, Errors>> Create(CultivabilityType cultivabilitytype, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(cultivabilitytype, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(cultivabilitytype);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        CultivabilityType lab = await CultivabilityTypes.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        CultivabilityTypes.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<CultivabilityType> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.CultivabilityTypes
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(CultivabilityType cultivabilitytype, CancellationToken cancellationToken)
    {
        CultivabilityTypes.Update(cultivabilitytype);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}