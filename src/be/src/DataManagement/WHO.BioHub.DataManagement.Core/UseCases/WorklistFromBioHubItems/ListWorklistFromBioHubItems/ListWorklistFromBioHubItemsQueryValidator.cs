using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;

public class ListWorklistFromBioHubItemsQueryValidator : AbstractValidator<ListWorklistFromBioHubItemsQuery>
{
    public ListWorklistFromBioHubItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}