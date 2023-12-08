using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratoryFromUserRequest;

public class CreateLaboratoryFromUserRequestCommandValidator : AbstractValidator<CreateLaboratoryFromUserRequestCommand>
{
    public CreateLaboratoryFromUserRequestCommandValidator()
    {
        RuleFor(cmd => cmd.InstituteName)
            .NotEmpty().WithMessage("'Institute Name' is required");

        RuleFor(cmd => cmd.CountryId)
            .NotEmpty().WithMessage("'Country Id' is required");

    }
}