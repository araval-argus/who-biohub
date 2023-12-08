using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.DeleteUserRequest;

public class DeleteUserRequestCommandValidator : AbstractValidator<DeleteUserRequestCommand>
{
    public DeleteUserRequestCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}