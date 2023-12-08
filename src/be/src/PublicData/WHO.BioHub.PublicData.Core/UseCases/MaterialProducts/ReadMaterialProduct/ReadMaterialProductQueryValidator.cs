using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ReadMaterialProduct;

public class ReadMaterialProductQueryValidator : AbstractValidator<ReadMaterialProductQuery>
{
    public ReadMaterialProductQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}