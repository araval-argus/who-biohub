using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.CreateWorklistFromBioHubHistoryItem;

public class CreateWorklistFromBioHubHistoryItemCommandValidator : AbstractValidator<CreateWorklistFromBioHubHistoryItemCommand>
{
    public CreateWorklistFromBioHubHistoryItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}