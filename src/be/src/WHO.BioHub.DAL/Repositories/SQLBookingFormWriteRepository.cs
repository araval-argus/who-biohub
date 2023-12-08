using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBookingFormWriteRepository : IBookingFormWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<BookingForm> BookingForms => _dbContext.BookingForms;

    public SQLBookingFormWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<BookingForm, Errors>> Create(BookingForm bookingform, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(bookingform, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(bookingform);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        BookingForm lab = await BookingForms.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        BookingForms.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<BookingForm> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BookingForms
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(BookingForm bookingform, CancellationToken cancellationToken)
    {
        BookingForms.Update(bookingform);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}