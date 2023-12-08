using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.DeleteMaterialType;

public class DeleteMaterialTypeCommandValidator : AbstractValidator<DeleteMaterialTypeCommand>
{
    public DeleteMaterialTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}