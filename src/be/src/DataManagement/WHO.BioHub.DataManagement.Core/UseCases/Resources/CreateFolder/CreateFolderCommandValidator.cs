using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateFolder;

public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
{
    public CreateFolderCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("Name is mandatory");
    }
}