using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;

namespace WHO.BioHub.DAL.Repositories;

public class SQLCourierReadRepository : ICourierReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLCourierReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Courier>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Couriers
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Courier> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Couriers
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<CourierHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken)
    {
        return await _dbContext.CouriersHistory
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.CourierId == id && l.LastOperationDate <= date)
            .OrderByDescending(x => x.LastOperationDate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}