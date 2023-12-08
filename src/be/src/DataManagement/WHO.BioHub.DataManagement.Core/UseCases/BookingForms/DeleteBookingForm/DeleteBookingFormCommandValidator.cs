using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.DeleteBookingForm;

public class DeleteBookingFormCommandValidator : AbstractValidator<DeleteBookingFormCommand>
{
    public DeleteBookingFormCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}