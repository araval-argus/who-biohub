using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ReadIsolationTechniqueType;

public class ReadIsolationTechniqueTypeQueryValidator : AbstractValidator<ReadIsolationTechniqueTypeQuery>
{
    public ReadIsolationTechniqueTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}