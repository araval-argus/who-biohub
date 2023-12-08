using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListDashboardSMTA1WorkflowItems;

public class ListDashboardSMTA1WorkflowItemsQueryValidator : AbstractValidator<ListDashboardSMTA1WorkflowItemsQuery>
{
    public ListDashboardSMTA1WorkflowItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}