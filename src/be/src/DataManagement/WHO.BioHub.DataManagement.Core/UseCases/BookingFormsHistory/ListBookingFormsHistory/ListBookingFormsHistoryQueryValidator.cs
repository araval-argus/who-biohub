using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ListBookingFormsHistory;

public class ListBookingFormsHistoryQueryValidator : AbstractValidator<ListBookingFormsHistoryQuery>
{
    public ListBookingFormsHistoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}