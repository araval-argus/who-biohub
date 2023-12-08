using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;

public class ListPriorityRequestTypesQueryValidator : AbstractValidator<ListPriorityRequestTypesQuery>
{
    public ListPriorityRequestTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}