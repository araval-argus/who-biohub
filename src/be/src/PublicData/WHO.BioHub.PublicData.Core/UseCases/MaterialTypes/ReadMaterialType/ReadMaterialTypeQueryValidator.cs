using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialTypes.ReadMaterialType;

public class ReadMaterialTypeQueryValidator : AbstractValidator<ReadMaterialTypeQuery>
{
    public ReadMaterialTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}