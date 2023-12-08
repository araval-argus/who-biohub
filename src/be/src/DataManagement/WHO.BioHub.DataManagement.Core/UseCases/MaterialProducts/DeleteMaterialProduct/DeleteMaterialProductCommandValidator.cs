using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.DeleteMaterialProduct;

public class DeleteMaterialProductCommandValidator : AbstractValidator<DeleteMaterialProductCommand>
{
    public DeleteMaterialProductCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}