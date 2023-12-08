using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.CreateWorklistToBioHubHistoryItem;

public class CreateWorklistToBioHubHistoryItemCommandValidator : AbstractValidator<CreateWorklistToBioHubHistoryItemCommand>
{
    public CreateWorklistToBioHubHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}