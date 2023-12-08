using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItems;

public class ListWorklistToBioHubItemsQueryValidator : AbstractValidator<ListWorklistToBioHubItemsQuery>
{
    public ListWorklistToBioHubItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}