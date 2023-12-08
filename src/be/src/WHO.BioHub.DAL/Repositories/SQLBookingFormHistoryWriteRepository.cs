using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBookingFormHistoryWriteRepository : IBookingFormHistoryWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<BookingFormHistory> BookingFormsHistory => _dbContext.BookingFormsHistory;

    public SQLBookingFormHistoryWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<BookingFormHistory, Errors>> Create(BookingFormHistory bookingformhistory, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(bookingformhistory, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(bookingformhistory);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        BookingFormHistory lab = await BookingFormsHistory.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        BookingFormsHistory.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<BookingFormHistory> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BookingFormsHistory
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(BookingFormHistory bookingformhistory, CancellationToken cancellationToken)
    {
        BookingFormsHistory.Update(bookingformhistory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}