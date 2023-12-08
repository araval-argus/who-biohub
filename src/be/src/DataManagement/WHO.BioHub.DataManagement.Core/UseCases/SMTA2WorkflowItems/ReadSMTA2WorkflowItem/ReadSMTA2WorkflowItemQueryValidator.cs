using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;

public class ReadSMTA2WorkflowItemQueryValidator : AbstractValidator<ReadSMTA2WorkflowItemQuery>
{
    public ReadSMTA2WorkflowItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}