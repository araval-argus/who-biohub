using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;

public class CreateIsolationTechniqueTypeCommandValidator : AbstractValidator<CreateIsolationTechniqueTypeCommand>
{
    public CreateIsolationTechniqueTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}