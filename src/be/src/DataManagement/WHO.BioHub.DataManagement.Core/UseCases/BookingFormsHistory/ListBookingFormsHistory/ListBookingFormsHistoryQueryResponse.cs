using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ListBookingFormsHistory;

public record struct ListBookingFormsHistoryQueryResponse(IEnumerable<BookingFormHistory> BookingFormsHistory) { }