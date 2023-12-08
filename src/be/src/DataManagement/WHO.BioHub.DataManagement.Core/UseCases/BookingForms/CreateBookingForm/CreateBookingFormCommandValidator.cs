using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.CreateBookingForm;

public class CreateBookingFormCommandValidator : AbstractValidator<CreateBookingFormCommand>
{
    public CreateBookingFormCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}