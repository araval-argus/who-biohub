using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.DeleteMaterial;

public class DeleteMaterialCommandValidator : AbstractValidator<DeleteMaterialCommand>
{
    public DeleteMaterialCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}