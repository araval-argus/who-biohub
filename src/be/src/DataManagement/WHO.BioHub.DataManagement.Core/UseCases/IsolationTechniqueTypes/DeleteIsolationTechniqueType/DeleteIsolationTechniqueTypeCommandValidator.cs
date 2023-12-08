using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.DeleteIsolationTechniqueType;

public class DeleteIsolationTechniqueTypeCommandValidator : AbstractValidator<DeleteIsolationTechniqueTypeCommand>
{
    public DeleteIsolationTechniqueTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}