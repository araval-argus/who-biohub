using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.DeleteMaterialShippingInformationHistory;

public class DeleteMaterialShippingInformationHistoryCommandValidator : AbstractValidator<DeleteMaterialShippingInformationHistoryCommand>
{
    public DeleteMaterialShippingInformationHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}