using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ReadBookingForm;

public class ReadBookingFormQueryValidator : AbstractValidator<ReadBookingFormQuery>
{
    public ReadBookingFormQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}