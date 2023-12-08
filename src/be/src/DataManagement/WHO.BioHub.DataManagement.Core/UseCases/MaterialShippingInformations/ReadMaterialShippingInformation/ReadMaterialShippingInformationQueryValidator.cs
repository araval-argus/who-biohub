using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ReadMaterialShippingInformation;

public class ReadMaterialShippingInformationQueryValidator : AbstractValidator<ReadMaterialShippingInformationQuery>
{
    public ReadMaterialShippingInformationQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}