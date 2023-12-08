using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ListMaterialProducts;

public class ListMaterialProductsQueryValidator : AbstractValidator<ListMaterialProductsQuery>
{
    public ListMaterialProductsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}