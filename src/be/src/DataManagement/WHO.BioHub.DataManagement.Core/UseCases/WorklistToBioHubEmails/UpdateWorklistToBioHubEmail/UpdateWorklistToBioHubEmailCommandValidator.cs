using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.UpdateWorklistToBioHubEmail;

public class UpdateWorklistToBioHubEmailCommandValidator : AbstractValidator<UpdateWorklistToBioHubEmailCommand>
{
    public UpdateWorklistToBioHubEmailCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}