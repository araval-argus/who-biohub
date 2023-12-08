using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ListMaterialClinicalDetails;

public class ListMaterialClinicalDetailsQueryValidator : AbstractValidator<ListMaterialClinicalDetailsQuery>
{
    public ListMaterialClinicalDetailsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}