using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public class ListMaterialsQueryValidator : AbstractValidator<ListMaterialsQuery>
{
    public ListMaterialsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}