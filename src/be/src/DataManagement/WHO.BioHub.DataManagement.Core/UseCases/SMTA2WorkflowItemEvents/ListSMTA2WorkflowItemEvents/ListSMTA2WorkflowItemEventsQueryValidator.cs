using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItemEvents.ListSMTA2WorkflowItemEvents;

public class ListSMTA2WorkflowItemEventsQueryValidator : AbstractValidator<ListSMTA2WorkflowItemEventsQuery>
{
    public ListSMTA2WorkflowItemEventsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}