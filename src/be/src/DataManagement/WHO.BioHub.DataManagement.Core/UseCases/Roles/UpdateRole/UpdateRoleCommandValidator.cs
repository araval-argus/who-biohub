using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");
        RuleFor(cmd => cmd.RoleType)
            .NotEmpty().WithMessage("'Role Type' is required");
    }
}