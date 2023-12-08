using FluentValidation;

namespace WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByCountry;

public class SearchLaboratoriesByCountryQueryValidator : AbstractValidator<SearchLaboratoriesByCountryQuery>
{
    public SearchLaboratoriesByCountryQueryValidator()
    {
        // TODO: complete here
        RuleFor(cmd => cmd.Country)
            .NotEmpty()
                .WithMessage("'Country' name is required");
    }
}