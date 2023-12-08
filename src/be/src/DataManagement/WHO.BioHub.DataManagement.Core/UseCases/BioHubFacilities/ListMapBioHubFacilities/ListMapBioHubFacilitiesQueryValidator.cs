using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;

public class ListMapBioHubFacilitiesQueryValidator : AbstractValidator<ListMapBioHubFacilitiesQuery>
{
    public ListMapBioHubFacilitiesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}