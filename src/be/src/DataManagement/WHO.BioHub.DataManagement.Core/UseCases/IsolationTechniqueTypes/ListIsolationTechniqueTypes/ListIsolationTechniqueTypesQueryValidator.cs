using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;

public class ListIsolationTechniqueTypesQueryValidator : AbstractValidator<ListIsolationTechniqueTypesQuery>
{
    public ListIsolationTechniqueTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}