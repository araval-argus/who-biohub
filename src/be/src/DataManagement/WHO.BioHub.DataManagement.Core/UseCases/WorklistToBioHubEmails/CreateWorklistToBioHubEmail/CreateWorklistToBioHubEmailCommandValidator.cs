using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;

public class CreateWorklistToBioHubEmailCommandValidator : AbstractValidator<CreateWorklistToBioHubEmailCommand>
{
    public CreateWorklistToBioHubEmailCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}