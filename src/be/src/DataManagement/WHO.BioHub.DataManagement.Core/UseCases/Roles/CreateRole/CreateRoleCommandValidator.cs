using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");
        RuleFor(cmd => cmd.RoleType)
            .NotEmpty().WithMessage("'Role Type' is required");
    }
}