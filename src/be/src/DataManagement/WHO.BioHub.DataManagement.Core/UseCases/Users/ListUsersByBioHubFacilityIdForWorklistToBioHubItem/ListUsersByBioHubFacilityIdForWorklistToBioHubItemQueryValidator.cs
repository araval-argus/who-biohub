using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;

public class ListUsersByBioHubFacilityIdForWorklistToBioHubItemQueryValidator : AbstractValidator<ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery>
{
    public ListUsersByBioHubFacilityIdForWorklistToBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}