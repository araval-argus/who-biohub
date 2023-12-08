using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItems;

public class ListDashboardWorklistFromBioHubItemsQueryValidator : AbstractValidator<ListDashboardWorklistFromBioHubItemsQuery>
{
    public ListDashboardWorklistFromBioHubItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}