using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ReadMaterialProduct;

public class ReadMaterialProductQueryValidator : AbstractValidator<ReadMaterialProductQuery>
{
    public ReadMaterialProductQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}