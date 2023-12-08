using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ReadBookingFormHistory;

public class ReadBookingFormHistoryQueryValidator : AbstractValidator<ReadBookingFormHistoryQuery>
{
    public ReadBookingFormHistoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}