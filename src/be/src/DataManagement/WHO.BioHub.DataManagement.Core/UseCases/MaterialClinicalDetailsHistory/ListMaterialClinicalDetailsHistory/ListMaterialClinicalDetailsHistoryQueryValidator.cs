using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ListMaterialClinicalDetailsHistory;

public class ListMaterialClinicalDetailsHistoryQueryValidator : AbstractValidator<ListMaterialClinicalDetailsHistoryQuery>
{
    public ListMaterialClinicalDetailsHistoryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}