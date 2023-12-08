using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ReadWorklistToBioHubEmail;

public class ReadWorklistToBioHubEmailQueryValidator : AbstractValidator<ReadWorklistToBioHubEmailQuery>
{
    public ReadWorklistToBioHubEmailQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}