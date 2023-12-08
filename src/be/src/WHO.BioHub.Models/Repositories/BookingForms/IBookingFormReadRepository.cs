using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.BookingForms;

public interface IBookingFormReadRepository
{
    Task<BookingForm> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BookingForm>> List(CancellationToken cancellationToken);
    Task<IEnumerable<BookingForm>> ListByCourierIdWithExtraInformation(Guid courierId, CancellationToken cancellationToken);
    Task<BookingForm> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken);
}
