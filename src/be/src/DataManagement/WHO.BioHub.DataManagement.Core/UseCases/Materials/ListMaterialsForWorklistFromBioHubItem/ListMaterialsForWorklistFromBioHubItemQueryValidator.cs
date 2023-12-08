using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public class ListMaterialsForWorklistFromBioHubItemQueryValidator : AbstractValidator<ListMaterialsForWorklistFromBioHubItemQuery>
{
    public ListMaterialsForWorklistFromBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}