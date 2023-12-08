using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;

public class ListSMTA1WorkflowItemsQueryValidator : AbstractValidator<ListSMTA1WorkflowItemsQuery>
{
    public ListSMTA1WorkflowItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}