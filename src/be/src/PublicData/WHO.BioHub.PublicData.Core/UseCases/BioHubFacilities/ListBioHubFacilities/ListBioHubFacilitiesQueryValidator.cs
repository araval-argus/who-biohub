using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public class ListBioHubFacilitiesQueryValidator : AbstractValidator<ListBioHubFacilitiesQuery>
{
    public ListBioHubFacilitiesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}