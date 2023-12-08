using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLIsolationTechniqueTypeWriteRepository : IIsolationTechniqueTypeWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<IsolationTechniqueType> IsolationTechniqueTypes => _dbContext.IsolationTechniqueTypes;

    public SQLIsolationTechniqueTypeWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<IsolationTechniqueType, Errors>> Create(IsolationTechniqueType isolationtechniquetype, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(isolationtechniquetype, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(isolationtechniquetype);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        IsolationTechniqueType lab = await IsolationTechniqueTypes.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        IsolationTechniqueTypes.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<IsolationTechniqueType> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.IsolationTechniqueTypes
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(IsolationTechniqueType isolationtechniquetype, CancellationToken cancellationToken)
    {
        IsolationTechniqueTypes.Update(isolationtechniquetype);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}