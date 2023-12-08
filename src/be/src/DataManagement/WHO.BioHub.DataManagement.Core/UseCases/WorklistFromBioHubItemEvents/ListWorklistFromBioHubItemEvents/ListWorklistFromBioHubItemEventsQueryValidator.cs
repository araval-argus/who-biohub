using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItemEvents.ListWorklistFromBioHubItemEvents;

public class ListWorklistFromBioHubItemEventsQueryValidator : AbstractValidator<ListWorklistFromBioHubItemEventsQuery>
{
    public ListWorklistFromBioHubItemEventsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}