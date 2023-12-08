using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItemEvents.ListWorklistToBioHubItemEvents;

public class ListWorklistToBioHubItemEventsQueryValidator : AbstractValidator<ListWorklistToBioHubItemEventsQuery>
{
    public ListWorklistToBioHubItemEventsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}