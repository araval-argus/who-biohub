using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.ListCountries;

public class ListCountriesQueryValidator : AbstractValidator<ListCountriesQuery>
{
    public ListCountriesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}