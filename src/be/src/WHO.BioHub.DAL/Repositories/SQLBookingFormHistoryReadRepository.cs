using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBookingFormHistoryReadRepository : IBookingFormHistoryReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLBookingFormHistoryReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookingFormHistory>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.BookingFormsHistory
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<BookingFormHistory> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BookingFormsHistory
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}