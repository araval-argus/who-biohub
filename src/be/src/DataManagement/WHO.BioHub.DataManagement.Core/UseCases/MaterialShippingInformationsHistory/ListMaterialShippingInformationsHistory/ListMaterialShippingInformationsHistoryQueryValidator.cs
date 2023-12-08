using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ListMaterialShippingInformationsHistory;

public class ListMaterialShippingInformationsHistoryQueryValidator : AbstractValidator<ListMaterialShippingInformationsHistoryQuery>
{
    public ListMaterialShippingInformationsHistoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}