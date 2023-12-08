using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Countries.ReadCountry;

public class ReadCountryQueryValidator : AbstractValidator<ReadCountryQuery>
{
    public ReadCountryQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}