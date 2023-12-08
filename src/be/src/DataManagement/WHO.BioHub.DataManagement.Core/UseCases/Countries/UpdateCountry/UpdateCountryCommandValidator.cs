using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.UpdateCountry;

public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}