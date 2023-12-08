using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.CreateMaterialShippingInformation;

public class CreateMaterialShippingInformationCommandValidator : AbstractValidator<CreateMaterialShippingInformationCommand>
{
    public CreateMaterialShippingInformationCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}