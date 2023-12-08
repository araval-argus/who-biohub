using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.CreateSMTA1WorkflowHistoryItem;

public class CreateSMTA1WorkflowHistoryItemCommandValidator : AbstractValidator<CreateSMTA1WorkflowHistoryItemCommand>
{
    public CreateSMTA1WorkflowHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}