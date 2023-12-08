using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public class ListMaterialsForWorklistToBioHubItemQueryValidator : AbstractValidator<ListMaterialsForWorklistToBioHubItemQuery>
{
    public ListMaterialsForWorklistToBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}