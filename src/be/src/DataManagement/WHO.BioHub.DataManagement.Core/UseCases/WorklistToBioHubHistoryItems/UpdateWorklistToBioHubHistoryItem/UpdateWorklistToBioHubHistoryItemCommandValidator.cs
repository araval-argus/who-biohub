using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.UpdateWorklistToBioHubHistoryItem;

public class UpdateWorklistToBioHubHistoryItemCommandValidator : AbstractValidator<UpdateWorklistToBioHubHistoryItemCommand>
{
    public UpdateWorklistToBioHubHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}