using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;

public class ListPriorityRequestTypesQueryValidator : AbstractValidator<ListPriorityRequestTypesQuery>
{
    public ListPriorityRequestTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}