using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ReadWorklistFromBioHubHistoryItem;

public class ReadWorklistFromBioHubHistoryItemQueryValidator : AbstractValidator<ReadWorklistFromBioHubHistoryItemQuery>
{
    public ReadWorklistFromBioHubHistoryItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}