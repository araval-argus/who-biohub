using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.CreateUserRequest;

public class CreateUserRequestCommandValidator : AbstractValidator<CreateUserRequestCommand>
{
    private readonly IUserRequestPublicReadRepository _userReadRepository;
    public CreateUserRequestCommandValidator(IUserRequestPublicReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;

        RuleFor(cmd => cmd.FirstName)
            .NotEmpty().WithMessage("'First Name' is required");
        RuleFor(cmd => cmd.LastName)
            .NotEmpty().WithMessage("'Last Name' is required");
        RuleFor(cmd => cmd.InstituteName)
            .NotEmpty().WithMessage("'Institute name' is required");
        RuleFor(cmd => cmd.Email)
            .NotEmpty().WithMessage("'Email' is required")
           .Must((e) => IsEmailAddressValid(e)).WithMessage("Invalid Email Address")
           .Must((e) => NotContainsWhiteSpaces(e)).WithMessage("Please remove white spaces")
           .MustAsync(async (command, email, token) => await EmailNotPresent(email, token)).WithMessage("Email already present");
        RuleFor(cmd => cmd.Purpose)
            .NotEmpty().WithMessage("'Purpose' is required");
        RuleFor(cmd => cmd.CountryId)
            .NotNull().WithMessage("'Country' is required");
        RuleFor(cmd => cmd.RoleId)
            .NotNull().WithMessage("'Role' is required");
        RuleFor(cmd => cmd.Status)
            .NotNull().WithMessage("'Status' is required");

        RuleFor(cmd => cmd.TermsAndConditionAccepted)
            .Must((c) => c == true).WithMessage("You must accept Terms And Conditions");
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