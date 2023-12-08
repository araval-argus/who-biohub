using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.ReadResourceFileToken;

public class ReadResourceFileTokenQueryValidator : AbstractValidator<ReadResourceFileTokenQuery>
{
    public ReadResourceFileTokenQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}