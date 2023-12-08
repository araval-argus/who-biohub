using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.DeleteMaterialClinicalDetail;

public class DeleteMaterialClinicalDetailCommandValidator : AbstractValidator<DeleteMaterialClinicalDetailCommand>
{
    public DeleteMaterialClinicalDetailCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}