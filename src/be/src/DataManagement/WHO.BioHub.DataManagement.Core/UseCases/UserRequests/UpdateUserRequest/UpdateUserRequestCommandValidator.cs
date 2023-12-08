using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;

public class UpdateUserRequestCommandValidator : AbstractValidator<UpdateUserRequestCommand>
{
    public UpdateUserRequestCommandValidator()
    {
        RuleFor(cmd => cmd.FirstName)
            .NotEmpty().WithMessage("'First Name' is required");
        RuleFor(cmd => cmd.LastName)
            .NotEmpty().WithMessage("'Last Name' is required");
        RuleFor(cmd => cmd.Email)
            .NotEmpty().WithMessage("'Email' is required")  
           .Must((e) => IsEmailAddressValid(e)).WithMessage("Invalid Email Address")
           .Must((e) => NotContainsWhiteSpaces(e)).WithMessage("Please remove white spaces"); 
        //RuleFor(cmd => cmd.Purpose)
        //    .NotEmpty().WithMessage("'Purpose' is required");
        //RuleFor(cmd => cmd.CountryId)
        //    .NotNull().WithMessage("'Country' is required");
        RuleFor(cmd => cmd.RoleId)
            .NotNull().WithMessage("'Role' is required");
        RuleFor(cmd => cmd.Status)
           .NotNull().WithMessage("'Status' is required");
        //RuleFor(cmd => cmd.TermsAndConditionAccepted)
        //    .Must((c) => c == true).WithMessage("You must accept Terms And Conditions");
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
}