using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DeleteSMTA2WorkflowHistoryItem;

public class DeleteSMTA2WorkflowHistoryItemCommandValidator : AbstractValidator<DeleteSMTA2WorkflowHistoryItemCommand>
{
    public DeleteSMTA2WorkflowHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}