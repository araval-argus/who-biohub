using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.UpdateSMTA1WorkflowHistoryItem;

public class UpdateSMTA1WorkflowHistoryItemCommandValidator : AbstractValidator<UpdateSMTA1WorkflowHistoryItemCommand>
{
    public UpdateSMTA1WorkflowHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}