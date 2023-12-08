using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ListMaterialShippingInformations;

public class ListMaterialShippingInformationsQueryValidator : AbstractValidator<ListMaterialShippingInformationsQuery>
{
    public ListMaterialShippingInformationsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}