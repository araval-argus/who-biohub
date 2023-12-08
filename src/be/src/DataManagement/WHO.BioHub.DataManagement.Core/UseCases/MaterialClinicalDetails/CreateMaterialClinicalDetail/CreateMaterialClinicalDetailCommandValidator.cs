using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.CreateMaterialClinicalDetail;

public class CreateMaterialClinicalDetailCommandValidator : AbstractValidator<CreateMaterialClinicalDetailCommand>
{
    public CreateMaterialClinicalDetailCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}