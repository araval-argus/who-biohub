using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DeleteWorklistFromBioHubItem;

public class DeleteWorklistFromBioHubItemCommandValidator : AbstractValidator<DeleteWorklistFromBioHubItemCommand>
{
    public DeleteWorklistFromBioHubItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}