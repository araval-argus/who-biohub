using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSpecimenTypeWriteRepository : ISpecimenTypeWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<SpecimenType> SpecimenTypes => _dbContext.SpecimenTypes;

    public SQLSpecimenTypeWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<SpecimenType, Errors>> Create(SpecimenType specimenttype, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(specimenttype, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(specimenttype);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        SpecimenType lab = await SpecimenTypes.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        SpecimenTypes.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<SpecimenType> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SpecimenTypes
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(SpecimenType specimenttype, CancellationToken cancellationToken)
    {
        SpecimenTypes.Update(specimenttype);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}