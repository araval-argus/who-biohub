using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterial;

public class ReadMaterialQueryValidator : AbstractValidator<ReadMaterialQuery>
{
    public ReadMaterialQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}