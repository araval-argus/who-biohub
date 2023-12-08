using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.UpdateMaterialShippingInformation;

public class UpdateMaterialShippingInformationCommandValidator : AbstractValidator<UpdateMaterialShippingInformationCommand>
{
    public UpdateMaterialShippingInformationCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}