using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;

public class ListCultivabilityTypesQueryValidator : AbstractValidator<ListCultivabilityTypesQuery>
{
    public ListCultivabilityTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}