using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;

public class ListIsolationTechniqueTypesQueryValidator : AbstractValidator<ListIsolationTechniqueTypesQuery>
{
    public ListIsolationTechniqueTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}