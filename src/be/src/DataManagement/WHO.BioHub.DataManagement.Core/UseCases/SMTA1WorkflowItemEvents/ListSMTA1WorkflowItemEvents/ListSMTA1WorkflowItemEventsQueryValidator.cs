using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItemEvents.ListSMTA1WorkflowItemEvents;

public class ListSMTA1WorkflowItemEventsQueryValidator : AbstractValidator<ListSMTA1WorkflowItemEventsQuery>
{
    public ListSMTA1WorkflowItemEventsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}