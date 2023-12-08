using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.BookingForms;

public interface IBookingFormWriteRepository
{
    Task<Either<BookingForm, Errors>> Create(BookingForm bookingform, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<BookingForm> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(BookingForm bookingform, CancellationToken cancellationToken);
}
