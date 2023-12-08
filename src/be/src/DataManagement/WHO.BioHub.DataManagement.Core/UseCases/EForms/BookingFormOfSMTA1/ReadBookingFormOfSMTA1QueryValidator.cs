using FluentValidation;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA1;

public class ReadBookingFormOfSMTA1QueryValidator : AbstractValidator<ReadBookingFormOfSMTA1Query>
{
    public ReadBookingFormOfSMTA1QueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}