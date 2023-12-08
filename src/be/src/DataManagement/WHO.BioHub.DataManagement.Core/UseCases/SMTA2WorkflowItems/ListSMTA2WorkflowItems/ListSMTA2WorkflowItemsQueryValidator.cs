using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;

public class ListSMTA2WorkflowItemsQueryValidator : AbstractValidator<ListSMTA2WorkflowItemsQuery>
{
    public ListSMTA2WorkflowItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}