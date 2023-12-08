using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Countries.ListCountries;

public class ListCountriesQueryValidator : AbstractValidator<ListCountriesQuery>
{
    public ListCountriesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}