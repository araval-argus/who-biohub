using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.UpdateWorklistFromBioHubHistoryItem;

public class UpdateWorklistFromBioHubHistoryItemCommandValidator : AbstractValidator<UpdateWorklistFromBioHubHistoryItemCommand>
{
    public UpdateWorklistFromBioHubHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}