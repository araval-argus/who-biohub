using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;

public class UpdateUserRequestStatusCommandValidator : AbstractValidator<UpdateUserRequestStatusCommand>
{
    public UpdateUserRequestStatusCommandValidator()
    {
        RuleFor(cmd => cmd.Message)
            .NotEmpty().WithMessage("'Message' is required")
            .MaximumLength(1000).WithMessage("Maximum lenght is 1000 characters");
    }
}