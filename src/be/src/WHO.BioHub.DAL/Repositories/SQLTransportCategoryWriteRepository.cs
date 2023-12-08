using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLTransportCategoryWriteRepository : ITransportCategoryWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<TransportCategory> TransportCategories => _dbContext.TransportCategories;

    public SQLTransportCategoryWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<TransportCategory, Errors>> Create(TransportCategory transportcategory, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(transportcategory, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(transportcategory);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        TransportCategory lab = await TransportCategories.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        TransportCategories.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<TransportCategory> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.TransportCategories
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(TransportCategory transportcategory, CancellationToken cancellationToken)
    {
        TransportCategories.Update(transportcategory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}