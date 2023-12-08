using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.CreateBookingFormHistory;

public record struct CreateBookingFormHistoryCommandResponse(BookingFormHistory BookingFormHistory) { }