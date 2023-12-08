using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DeleteSMTA2WorkflowItem;

public class DeleteSMTA2WorkflowItemCommandValidator : AbstractValidator<DeleteSMTA2WorkflowItemCommand>
{
    public DeleteSMTA2WorkflowItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}