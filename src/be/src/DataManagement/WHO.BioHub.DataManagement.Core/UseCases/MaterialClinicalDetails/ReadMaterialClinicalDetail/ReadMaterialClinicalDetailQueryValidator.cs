using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ReadMaterialClinicalDetail;

public class ReadMaterialClinicalDetailQueryValidator : AbstractValidator<ReadMaterialClinicalDetailQuery>
{
    public ReadMaterialClinicalDetailQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}