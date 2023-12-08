using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItems;

public class ListDashboardSMTA2WorkflowItemsQueryValidator : AbstractValidator<ListDashboardSMTA2WorkflowItemsQuery>
{
    public ListDashboardSMTA2WorkflowItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}