using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.BookingFormsHistory;

public interface IBookingFormHistoryWriteRepository
{
    Task<Either<BookingFormHistory, Errors>> Create(BookingFormHistory bookingformhistory, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<BookingFormHistory> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(BookingFormHistory bookingformhistory, CancellationToken cancellationToken);
}
