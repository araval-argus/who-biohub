using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WHO.BioHub.Models.Repositories.Users;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUserFromUserRequest;

public class CreateUserFromUserRequestCommandValidator : AbstractValidator<CreateUserFromUserRequestCommand>
{
    private readonly IUserReadRepository _userReadRepository;
    public CreateUserFromUserRequestCommandValidator(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;

        RuleFor(cmd => cmd.FirstName)
            .NotEmpty().WithMessage("'First Name' is required");
        RuleFor(cmd => cmd.LastName)
            .NotEmpty().WithMessage("'Last Name' is required");
        RuleFor(cmd => cmd.RoleId)
            .NotNull().WithMessage("'Role' is required");
        RuleFor(cmd => cmd.Email)
            .NotEmpty().WithMessage("'Email' is required")
            .Must((e) => IsEmailAddressValid(e)).WithMessage("Invalid Email Address")
            .Must((e) => NotContainsWhiteSpaces(e)).WithMessage("Please remove white spaces")
            .MustAsync(async (command, email, token) => await EmailNotPresent(email, token)).WithMessage("Email already present");

    }

    protected bool IsEmailAddressValid(string email)
    {
        return (new EmailAddressAttribute().IsValid(email));
    }

    protected bool NotContainsWhiteSpaces(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return true;
        }
        return email.IndexOf(" ") == -1;
    }

    protected async Task<bool> EmailNotPresent(string email, CancellationToken token)
    {
        return !(await _userReadRepository.EmailPresent(email, token));
    }
}