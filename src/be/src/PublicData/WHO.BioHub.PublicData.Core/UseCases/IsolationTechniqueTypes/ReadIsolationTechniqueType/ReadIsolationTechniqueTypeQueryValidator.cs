using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ReadIsolationTechniqueType;

public class ReadIsolationTechniqueTypeQueryValidator : AbstractValidator<ReadIsolationTechniqueTypeQuery>
{
    public ReadIsolationTechniqueTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}