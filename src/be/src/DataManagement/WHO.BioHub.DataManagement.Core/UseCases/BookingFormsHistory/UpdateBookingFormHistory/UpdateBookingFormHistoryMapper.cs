using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.UpdateBookingFormHistory;

public interface IUpdateBookingFormHistoryMapper
{
    BookingFormHistory Map(BookingFormHistory bookingformhistory, UpdateBookingFormHistoryCommand command);
}

public class UpdateBookingFormHistoryMapper : IUpdateBookingFormHistoryMapper
{
    public BookingFormHistory Map(BookingFormHistory bookingformhistory, UpdateBookingFormHistoryCommand command)
    {
        // TODO: Implement mapper

        bookingformhistory.Id = command.Id;
        bookingformhistory.CreationDate = DateTime.UtcNow;

        // ...

        bookingformhistory.DeletedOn = null;

        return bookingformhistory;
    }
}