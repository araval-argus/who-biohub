using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItems;

public class ListDashboardWorklistToBioHubItemsQueryValidator : AbstractValidator<ListDashboardWorklistToBioHubItemsQuery>
{
    public ListDashboardWorklistToBioHubItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}