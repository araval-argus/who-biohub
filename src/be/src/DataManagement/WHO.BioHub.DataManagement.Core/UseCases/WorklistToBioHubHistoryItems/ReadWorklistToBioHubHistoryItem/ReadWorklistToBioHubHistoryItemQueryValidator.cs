using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;

public class ReadWorklistToBioHubHistoryItemQueryValidator : AbstractValidator<ReadWorklistToBioHubHistoryItemQuery>
{
    public ReadWorklistToBioHubHistoryItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}