using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}