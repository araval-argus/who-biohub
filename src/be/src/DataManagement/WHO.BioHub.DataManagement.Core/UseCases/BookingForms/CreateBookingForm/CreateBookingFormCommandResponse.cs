using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.CreateBookingForm;

public record struct CreateBookingFormCommandResponse(BookingForm BookingForm) { }