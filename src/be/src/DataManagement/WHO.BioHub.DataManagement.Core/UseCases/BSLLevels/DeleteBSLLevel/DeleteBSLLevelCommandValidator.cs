using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.DeleteBSLLevel;

public class DeleteBSLLevelCommandValidator : AbstractValidator<DeleteBSLLevelCommand>
{
    public DeleteBSLLevelCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}