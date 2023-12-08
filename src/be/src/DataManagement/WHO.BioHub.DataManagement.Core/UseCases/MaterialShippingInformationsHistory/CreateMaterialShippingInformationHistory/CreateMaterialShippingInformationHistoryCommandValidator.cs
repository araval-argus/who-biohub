using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.CreateMaterialShippingInformationHistory;

public class CreateMaterialShippingInformationHistoryCommandValidator : AbstractValidator<CreateMaterialShippingInformationHistoryCommand>
{
    public CreateMaterialShippingInformationHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}