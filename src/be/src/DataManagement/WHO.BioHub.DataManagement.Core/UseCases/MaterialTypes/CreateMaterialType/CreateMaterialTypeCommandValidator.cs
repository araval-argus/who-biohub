using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;

public class CreateMaterialTypeCommandValidator : AbstractValidator<CreateMaterialTypeCommand>
{
    public CreateMaterialTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}