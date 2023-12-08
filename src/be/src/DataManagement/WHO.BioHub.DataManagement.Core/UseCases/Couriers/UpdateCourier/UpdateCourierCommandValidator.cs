using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WHO.BioHub.Models.Repositories.Users;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.UpdateCourier;

public class UpdateCourierCommandValidator : AbstractValidator<UpdateCourierCommand>
{
    private readonly IUserReadRepository _userReadRepository;
    public UpdateCourierCommandValidator(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
        RuleFor(cmd => cmd.Name)
          .NotEmpty().WithMessage("'Name' is required");

        RuleFor(cmd => cmd.CountryId)
          .NotEmpty().WithMessage("'CountryId' is required");

        RuleFor(cmd => cmd.Address)
          .NotEmpty().WithMessage("'Address' is required");

        RuleFor(cmd => cmd.BusinessPhone)
          .NotEmpty().WithMessage("'Business Phone' is required");

        RuleFor(cmd => cmd.WHOAccountNumber)
          .NotEmpty().WithMessage("'WHO Account Number' is required");

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