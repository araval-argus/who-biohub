using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DeleteWorklistToBioHubItem;

public class DeleteWorklistToBioHubItemCommandValidator : AbstractValidator<DeleteWorklistToBioHubItemCommand>
{
    public DeleteWorklistToBioHubItemCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}