using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.DeleteMaterialClinicalDetailHistory;

public class DeleteMaterialClinicalDetailHistoryCommandValidator : AbstractValidator<DeleteMaterialClinicalDetailHistoryCommand>
{
    public DeleteMaterialClinicalDetailHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}