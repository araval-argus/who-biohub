using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.DeleteWorklistToBioHubEmail;

public class DeleteWorklistToBioHubEmailCommandValidator : AbstractValidator<DeleteWorklistToBioHubEmailCommand>
{
    public DeleteWorklistToBioHubEmailCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}