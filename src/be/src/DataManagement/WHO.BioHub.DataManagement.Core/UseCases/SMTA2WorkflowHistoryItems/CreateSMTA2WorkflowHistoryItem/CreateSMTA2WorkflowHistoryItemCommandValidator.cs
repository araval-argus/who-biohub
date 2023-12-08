using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.CreateSMTA2WorkflowHistoryItem;

public class CreateSMTA2WorkflowHistoryItemCommandValidator : AbstractValidator<CreateSMTA2WorkflowHistoryItemCommand>
{
    public CreateSMTA2WorkflowHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}