using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}