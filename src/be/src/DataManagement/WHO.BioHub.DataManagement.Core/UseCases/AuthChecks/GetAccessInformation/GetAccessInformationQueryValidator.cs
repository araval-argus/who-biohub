using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;

public class GetAccessInformationQueryValidator : AbstractValidator<GetAccessInformationQuery>
{
    public GetAccessInformationQueryValidator()
    {
        RuleFor(cmd => cmd.Email)
            .NotEmpty().WithMessage("Email is required")
            .NotNull().WithMessage("Email is required");

        RuleFor(cmd => cmd.ExternalId)
            .NotNull().WithMessage("ExternalId is required");
    }
}