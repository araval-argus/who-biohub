using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.UpdateBookingForm;

public interface IUpdateBookingFormMapper
{
    BookingForm Map(BookingForm bookingform, UpdateBookingFormCommand command);
}

public class UpdateBookingFormMapper : IUpdateBookingFormMapper
{
    public BookingForm Map(BookingForm bookingform, UpdateBookingFormCommand command)
    {
        // TODO: Implement mapper

        bookingform.Id = command.Id;
        bookingform.CreationDate = DateTime.UtcNow;

        // ...

        bookingform.DeletedOn = null;

        return bookingform;
    }
}