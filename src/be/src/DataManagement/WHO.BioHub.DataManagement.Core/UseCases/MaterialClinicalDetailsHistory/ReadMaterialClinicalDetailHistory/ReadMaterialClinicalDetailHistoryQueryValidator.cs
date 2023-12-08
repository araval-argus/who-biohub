using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ReadMaterialClinicalDetailHistory;

public class ReadMaterialClinicalDetailHistoryQueryValidator : AbstractValidator<ReadMaterialClinicalDetailHistoryQuery>
{
    public ReadMaterialClinicalDetailHistoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}