using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ListSMTA2WorkflowHistoryItems;

public class ListSMTA2WorkflowHistoryItemsQueryValidator : AbstractValidator<ListSMTA2WorkflowHistoryItemsQuery>
{
    public ListSMTA2WorkflowHistoryItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}