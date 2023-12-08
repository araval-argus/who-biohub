using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;

public class ListIsolationHostTypesQueryValidator : AbstractValidator<ListIsolationHostTypesQuery>
{
    public ListIsolationHostTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}