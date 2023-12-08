using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.BookingFormsHistory;

public interface IBookingFormHistoryReadRepository
{
    Task<BookingFormHistory> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BookingFormHistory>> List(CancellationToken cancellationToken);
}
