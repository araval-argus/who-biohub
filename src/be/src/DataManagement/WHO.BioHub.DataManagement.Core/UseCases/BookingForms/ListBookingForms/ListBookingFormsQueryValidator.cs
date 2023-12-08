using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForms;

public class ListBookingFormsQueryValidator : AbstractValidator<ListBookingFormsQuery>
{
    public ListBookingFormsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}