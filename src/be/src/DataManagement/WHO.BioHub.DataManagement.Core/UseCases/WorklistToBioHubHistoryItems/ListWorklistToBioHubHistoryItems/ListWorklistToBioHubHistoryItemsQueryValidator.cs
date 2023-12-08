using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ListWorklistToBioHubHistoryItems;

public class ListWorklistToBioHubHistoryItemsQueryValidator : AbstractValidator<ListWorklistToBioHubHistoryItemsQuery>
{
    public ListWorklistToBioHubHistoryItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}