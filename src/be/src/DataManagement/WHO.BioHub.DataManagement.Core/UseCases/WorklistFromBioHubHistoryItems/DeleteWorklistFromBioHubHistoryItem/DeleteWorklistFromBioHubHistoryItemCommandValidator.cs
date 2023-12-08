using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DeleteWorklistFromBioHubHistoryItem;

public class DeleteWorklistFromBioHubHistoryItemCommandValidator : AbstractValidator<DeleteWorklistFromBioHubHistoryItemCommand>
{
    public DeleteWorklistFromBioHubHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}