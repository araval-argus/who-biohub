using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.MapBioHubFacilities.ListMapBioHubFacilities;

public class ListMapBioHubFacilitiesQueryValidator : AbstractValidator<ListMapBioHubFacilitiesQuery>
{
    public ListMapBioHubFacilitiesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}