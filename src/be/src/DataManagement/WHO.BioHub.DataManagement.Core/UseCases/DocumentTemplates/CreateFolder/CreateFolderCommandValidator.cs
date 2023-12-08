using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CreateFolder;

public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
{
    public CreateFolderCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("Name is mandatory");
    }
}