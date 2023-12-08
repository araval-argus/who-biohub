using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;

public class CreateUserRequestStatusCommandValidator : AbstractValidator<CreateUserRequestStatusCommand>
{
    public CreateUserRequestStatusCommandValidator()
    {
        RuleFor(cmd => cmd.Message)
            .NotEmpty().WithMessage("'Message' is required")
            .MaximumLength(1000).WithMessage("Maximum lenght is 1000 characters");
    }
}