using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DeleteWorklistToBioHubHistoryItem;

public class DeleteWorklistToBioHubHistoryItemCommandValidator : AbstractValidator<DeleteWorklistToBioHubHistoryItemCommand>
{
    public DeleteWorklistToBioHubHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}