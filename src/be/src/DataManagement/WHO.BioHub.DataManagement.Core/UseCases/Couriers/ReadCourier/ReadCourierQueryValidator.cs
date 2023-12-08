using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.ReadCourier;

public class ReadCourierQueryValidator : AbstractValidator<ReadCourierQuery>
{
    public ReadCourierQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}