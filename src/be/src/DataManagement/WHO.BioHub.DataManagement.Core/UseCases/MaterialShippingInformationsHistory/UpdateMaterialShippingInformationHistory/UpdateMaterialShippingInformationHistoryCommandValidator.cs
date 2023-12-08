using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.UpdateMaterialShippingInformationHistory;

public class UpdateMaterialShippingInformationHistoryCommandValidator : AbstractValidator<UpdateMaterialShippingInformationHistoryCommand>
{
    public UpdateMaterialShippingInformationHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}