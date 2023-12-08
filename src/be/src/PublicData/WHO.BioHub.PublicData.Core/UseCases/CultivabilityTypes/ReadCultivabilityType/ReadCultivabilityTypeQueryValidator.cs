using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;

public class ReadCultivabilityTypeQueryValidator : AbstractValidator<ReadCultivabilityTypeQuery>
{
    public ReadCultivabilityTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}