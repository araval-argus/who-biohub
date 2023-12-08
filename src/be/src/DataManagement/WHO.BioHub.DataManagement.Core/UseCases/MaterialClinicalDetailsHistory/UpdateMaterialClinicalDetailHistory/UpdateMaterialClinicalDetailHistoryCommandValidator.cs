using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.UpdateMaterialClinicalDetailHistory;

public class UpdateMaterialClinicalDetailHistoryCommandValidator : AbstractValidator<UpdateMaterialClinicalDetailHistoryCommand>
{
    public UpdateMaterialClinicalDetailHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}