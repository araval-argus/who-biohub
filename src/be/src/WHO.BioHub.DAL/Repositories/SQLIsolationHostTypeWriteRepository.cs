using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLIsolationHostTypeWriteRepository : IIsolationHostTypeWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<IsolationHostType> IsolationHostTypes => _dbContext.IsolationHostTypes;

    public SQLIsolationHostTypeWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<IsolationHostType, Errors>> Create(IsolationHostType isolationhosttype, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(isolationhosttype, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(isolationhosttype);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        IsolationHostType lab = await IsolationHostTypes.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        IsolationHostTypes.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<IsolationHostType> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.IsolationHostTypes
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(IsolationHostType isolationhosttype, CancellationToken cancellationToken)
    {
        IsolationHostTypes.Update(isolationhosttype);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}