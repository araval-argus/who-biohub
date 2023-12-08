using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;

public class ReadSMTA1WorkflowItemQueryValidator : AbstractValidator<ReadSMTA1WorkflowItemQuery>
{
    public ReadSMTA1WorkflowItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}