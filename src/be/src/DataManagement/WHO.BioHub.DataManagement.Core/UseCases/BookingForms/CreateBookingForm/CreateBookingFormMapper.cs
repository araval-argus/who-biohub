using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.CreateBookingForm;

public interface ICreateBookingFormMapper
{
    BookingForm Map(CreateBookingFormCommand command);
}

public class CreateBookingFormMapper : ICreateBookingFormMapper
{
    public BookingForm Map(CreateBookingFormCommand command)
    {        

        BookingForm bookingform = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return bookingform;
    }
}