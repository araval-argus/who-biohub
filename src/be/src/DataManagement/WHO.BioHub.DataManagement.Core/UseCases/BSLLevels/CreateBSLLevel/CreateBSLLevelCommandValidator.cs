using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;

public class CreateBSLLevelCommandValidator : AbstractValidator<CreateBSLLevelCommand>
{
    public CreateBSLLevelCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");

        RuleFor(cmd => cmd.Code)
            .NotEmpty().WithMessage("'Code' is required");

        RuleFor(cmd => cmd.Description)
            .NotEmpty().WithMessage("'Description' is required");

    }
}