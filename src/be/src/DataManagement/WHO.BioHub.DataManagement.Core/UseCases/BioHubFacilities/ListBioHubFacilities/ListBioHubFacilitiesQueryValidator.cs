using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public class ListBioHubFacilitiesQueryValidator : AbstractValidator<ListBioHubFacilitiesQuery>
{
    public ListBioHubFacilitiesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}