using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;

public class ListIsolationHostTypesQueryValidator : AbstractValidator<ListIsolationHostTypesQuery>
{
    public ListIsolationHostTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}