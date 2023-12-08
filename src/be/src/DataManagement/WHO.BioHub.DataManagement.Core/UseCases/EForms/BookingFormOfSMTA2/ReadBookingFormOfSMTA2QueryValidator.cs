using FluentValidation;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA2;

public class ReadBookingFormOfSMTA2QueryValidator : AbstractValidator<ReadBookingFormOfSMTA2Query>
{
    public ReadBookingFormOfSMTA2QueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}