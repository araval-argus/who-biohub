using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Roles.ListRoles;

public class ListRolesQueryValidator : AbstractValidator<ListRolesQuery>
{
    public ListRolesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}