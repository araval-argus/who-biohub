using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;

public class UpdateIsolationTechniqueTypeCommandValidator : AbstractValidator<UpdateIsolationTechniqueTypeCommand>
{
    public UpdateIsolationTechniqueTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}