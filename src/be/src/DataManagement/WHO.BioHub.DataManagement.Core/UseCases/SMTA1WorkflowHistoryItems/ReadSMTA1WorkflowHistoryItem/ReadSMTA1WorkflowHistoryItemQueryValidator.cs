using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;

public class ReadSMTA1WorkflowHistoryItemQueryValidator : AbstractValidator<ReadSMTA1WorkflowHistoryItemQuery>
{
    public ReadSMTA1WorkflowHistoryItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}