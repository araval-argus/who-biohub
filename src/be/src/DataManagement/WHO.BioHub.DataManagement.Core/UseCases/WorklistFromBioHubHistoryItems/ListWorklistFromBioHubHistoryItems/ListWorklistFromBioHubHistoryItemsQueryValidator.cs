using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ListWorklistFromBioHubHistoryItems;

public class ListWorklistFromBioHubHistoryItemsQueryValidator : AbstractValidator<ListWorklistFromBioHubHistoryItemsQuery>
{
    public ListWorklistFromBioHubHistoryItemsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}