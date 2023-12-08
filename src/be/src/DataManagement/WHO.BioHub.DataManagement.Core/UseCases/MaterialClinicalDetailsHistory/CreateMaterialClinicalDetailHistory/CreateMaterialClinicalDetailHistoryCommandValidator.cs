using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.CreateMaterialClinicalDetailHistory;

public class CreateMaterialClinicalDetailHistoryCommandValidator : AbstractValidator<CreateMaterialClinicalDetailHistoryCommand>
{
    public CreateMaterialClinicalDetailHistoryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}