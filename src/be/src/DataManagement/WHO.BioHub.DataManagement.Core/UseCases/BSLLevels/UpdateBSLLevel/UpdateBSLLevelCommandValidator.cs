using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;

public class UpdateBSLLevelCommandValidator : AbstractValidator<UpdateBSLLevelCommand>
{
    public UpdateBSLLevelCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");

        RuleFor(cmd => cmd.Code)
            .NotEmpty().WithMessage("'Code' is required");

        RuleFor(cmd => cmd.Description)
            .NotEmpty().WithMessage("'Description' is required");
    }
}