using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.DeleteUserRequestStatus;

public class DeleteUserRequestStatusCommandValidator : AbstractValidator<DeleteUserRequestStatusCommand>
{
    public DeleteUserRequestStatusCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}