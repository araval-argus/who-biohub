using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;

public class ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryValidator : AbstractValidator<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery>
{
    public ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}