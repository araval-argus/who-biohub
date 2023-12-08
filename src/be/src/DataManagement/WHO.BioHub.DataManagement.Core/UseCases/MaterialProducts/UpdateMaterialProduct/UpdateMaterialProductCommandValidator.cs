using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.UpdateMaterialProduct;

public class UpdateMaterialProductCommandValidator : AbstractValidator<UpdateMaterialProductCommand>
{
    public UpdateMaterialProductCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}