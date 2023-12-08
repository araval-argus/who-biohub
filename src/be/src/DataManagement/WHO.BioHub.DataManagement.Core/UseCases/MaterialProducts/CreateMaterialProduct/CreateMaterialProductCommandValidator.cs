using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;

public class CreateMaterialProductCommandValidator : AbstractValidator<CreateMaterialProductCommand>
{
    public CreateMaterialProductCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}