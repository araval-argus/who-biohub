using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.UpdateMaterialClinicalDetail;

public class UpdateMaterialClinicalDetailCommandValidator : AbstractValidator<UpdateMaterialClinicalDetailCommand>
{
    public UpdateMaterialClinicalDetailCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}