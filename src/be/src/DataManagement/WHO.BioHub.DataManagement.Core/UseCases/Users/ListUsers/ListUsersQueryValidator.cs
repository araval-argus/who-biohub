using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;

public class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
{
    public ListUsersQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}