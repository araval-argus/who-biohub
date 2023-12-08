using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.UpdateBookingForm;

public class UpdateBookingFormCommandValidator : AbstractValidator<UpdateBookingFormCommand>
{
    public UpdateBookingFormCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}