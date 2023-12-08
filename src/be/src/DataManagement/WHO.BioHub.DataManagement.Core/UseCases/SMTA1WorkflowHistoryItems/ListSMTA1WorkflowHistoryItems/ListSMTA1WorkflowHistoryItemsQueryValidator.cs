using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ListSMTA1WorkflowHistoryItems;

public class ListSMTA1WorkflowHistoryItemsQueryValidator : AbstractValidator<ListSMTA1WorkflowHistoryItemsQuery>
{
    public ListSMTA1WorkflowHistoryItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}