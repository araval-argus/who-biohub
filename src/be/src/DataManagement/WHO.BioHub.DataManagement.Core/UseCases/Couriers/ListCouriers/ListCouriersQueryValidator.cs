using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ListCouriers;

public class ListCouriersQueryValidator : AbstractValidator<ListCouriersQuery>
{
    public ListCouriersQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}