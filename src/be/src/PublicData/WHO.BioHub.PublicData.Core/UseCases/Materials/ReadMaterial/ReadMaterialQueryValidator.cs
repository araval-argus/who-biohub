using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ReadMaterial;

public class ReadMaterialQueryValidator : AbstractValidator<ReadMaterialQuery>
{
    public ReadMaterialQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}