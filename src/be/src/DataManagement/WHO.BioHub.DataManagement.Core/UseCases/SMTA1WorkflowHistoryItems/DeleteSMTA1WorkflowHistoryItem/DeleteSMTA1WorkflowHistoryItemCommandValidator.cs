using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DeleteSMTA1WorkflowHistoryItem;

public class DeleteSMTA1WorkflowHistoryItemCommandValidator : AbstractValidator<DeleteSMTA1WorkflowHistoryItemCommand>
{
    public DeleteSMTA1WorkflowHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}