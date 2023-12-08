using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.DeleteBookingFormHistory;

public class DeleteBookingFormHistoryCommandValidator : AbstractValidator<DeleteBookingFormHistoryCommand>
{
    public DeleteBookingFormHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}