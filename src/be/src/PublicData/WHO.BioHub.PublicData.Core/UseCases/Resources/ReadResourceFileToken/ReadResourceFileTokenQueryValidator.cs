using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Resources.ReadResourceFileToken;

public class ReadResourceFileTokenQueryValidator : AbstractValidator<ReadResourceFileTokenQuery>
{
    public ReadResourceFileTokenQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}