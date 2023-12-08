using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.DeleteMaterialShippingInformation;

public class DeleteMaterialShippingInformationCommandValidator : AbstractValidator<DeleteMaterialShippingInformationCommand>
{
    public DeleteMaterialShippingInformationCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}