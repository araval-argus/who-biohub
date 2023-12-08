using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListCourierUsers;

public class ListCourierUsersForWorklistToBioHubItemQueryValidator : AbstractValidator<ListCourierUsersForWorklistToBioHubItemQuery>
{
    public ListCourierUsersForWorklistToBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}