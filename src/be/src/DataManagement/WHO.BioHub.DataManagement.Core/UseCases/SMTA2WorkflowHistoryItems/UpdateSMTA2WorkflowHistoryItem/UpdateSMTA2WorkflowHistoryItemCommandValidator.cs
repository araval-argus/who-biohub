using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.UpdateSMTA2WorkflowHistoryItem;

public class UpdateSMTA2WorkflowHistoryItemCommandValidator : AbstractValidator<UpdateSMTA2WorkflowHistoryItemCommand>
{
    public UpdateSMTA2WorkflowHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}