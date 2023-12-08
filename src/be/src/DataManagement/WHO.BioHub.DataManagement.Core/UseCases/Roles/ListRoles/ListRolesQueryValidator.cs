using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.ListRoles;

public class ListRolesQueryValidator : AbstractValidator<ListRolesQuery>
{
    public ListRolesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}