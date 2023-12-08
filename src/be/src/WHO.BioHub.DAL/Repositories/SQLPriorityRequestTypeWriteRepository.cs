using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLPriorityRequestTypeWriteRepository : IPriorityRequestTypeWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<PriorityRequestType> PriorityRequestTypes => _dbContext.PriorityRequestTypes;

    public SQLPriorityRequestTypeWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<PriorityRequestType, Errors>> Create(PriorityRequestType priorityrequesttype, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(priorityrequesttype, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(priorityrequesttype);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        PriorityRequestType lab = await PriorityRequestTypes.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        PriorityRequestTypes.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<PriorityRequestType> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.PriorityRequestTypes
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(PriorityRequestType priorityrequesttype, CancellationToken cancellationToken)
    {
        PriorityRequestTypes.Update(priorityrequesttype);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}