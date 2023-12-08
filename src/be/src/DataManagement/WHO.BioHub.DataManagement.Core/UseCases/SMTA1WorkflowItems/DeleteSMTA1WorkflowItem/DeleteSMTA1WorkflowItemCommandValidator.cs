using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DeleteSMTA1WorkflowItem;

public class DeleteSMTA1WorkflowItemCommandValidator : AbstractValidator<DeleteSMTA1WorkflowItemCommand>
{
    public DeleteSMTA1WorkflowItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}