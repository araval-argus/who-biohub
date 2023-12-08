using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;

public class ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryValidator : AbstractValidator<ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery>
{
    public ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}