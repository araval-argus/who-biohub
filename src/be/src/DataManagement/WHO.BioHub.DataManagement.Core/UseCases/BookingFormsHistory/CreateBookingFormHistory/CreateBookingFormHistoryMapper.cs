using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.CreateBookingFormHistory;

public interface ICreateBookingFormHistoryMapper
{
    BookingFormHistory Map(CreateBookingFormHistoryCommand command);
}

public class CreateBookingFormHistoryMapper : ICreateBookingFormHistoryMapper
{
    public BookingFormHistory Map(CreateBookingFormHistoryCommand command)
    {
        // TODO: Implement mapper

        BookingFormHistory bookingformhistory = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return bookingformhistory;
    }
}