using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;

public class ReadSMTA2WorkflowHistoryItemQueryValidator : AbstractValidator<ReadSMTA2WorkflowHistoryItemQuery>
{
    public ReadSMTA2WorkflowHistoryItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}