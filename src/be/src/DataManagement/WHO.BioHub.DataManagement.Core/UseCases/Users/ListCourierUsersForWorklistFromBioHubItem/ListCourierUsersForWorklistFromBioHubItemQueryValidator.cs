using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListCourierUsers;

public class ListCourierUsersForWorklistFromBioHubItemQueryValidator : AbstractValidator<ListCourierUsersForWorklistFromBioHubItemQuery>
{
    public ListCourierUsersForWorklistFromBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}