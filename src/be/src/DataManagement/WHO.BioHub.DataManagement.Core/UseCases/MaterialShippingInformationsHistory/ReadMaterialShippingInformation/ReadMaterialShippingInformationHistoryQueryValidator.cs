using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ReadMaterialShippingInformationHistory;

public class ReadMaterialShippingInformationHistoryQueryValidator : AbstractValidator<ReadMaterialShippingInformationHistoryQuery>
{
    public ReadMaterialShippingInformationHistoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}