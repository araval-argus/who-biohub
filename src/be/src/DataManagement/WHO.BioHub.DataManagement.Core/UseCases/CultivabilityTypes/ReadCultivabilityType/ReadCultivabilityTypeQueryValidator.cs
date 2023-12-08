using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;

public class ReadCultivabilityTypeQueryValidator : AbstractValidator<ReadCultivabilityTypeQuery>
{
    public ReadCultivabilityTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}