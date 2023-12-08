using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ListMaterials;

public class ListMaterialsQueryValidator : AbstractValidator<ListMaterialsQuery>
{
    public ListMaterialsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}