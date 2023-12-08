using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}