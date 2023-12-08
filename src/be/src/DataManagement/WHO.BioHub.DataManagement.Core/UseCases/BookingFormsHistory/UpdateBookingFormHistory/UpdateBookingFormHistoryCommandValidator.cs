using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.UpdateBookingFormHistory;

public class UpdateBookingFormHistoryCommandValidator : AbstractValidator<UpdateBookingFormHistoryCommand>
{
    public UpdateBookingFormHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}