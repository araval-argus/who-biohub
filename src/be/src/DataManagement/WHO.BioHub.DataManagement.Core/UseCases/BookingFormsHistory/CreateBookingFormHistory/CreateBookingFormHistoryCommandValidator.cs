using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.CreateBookingFormHistory;

public class CreateBookingFormHistoryCommandValidator : AbstractValidator<CreateBookingFormHistoryCommand>
{
    public CreateBookingFormHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}