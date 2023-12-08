using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.ReadCountry;

public class ReadCountryQueryValidator : AbstractValidator<ReadCountryQuery>
{
    public ReadCountryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}