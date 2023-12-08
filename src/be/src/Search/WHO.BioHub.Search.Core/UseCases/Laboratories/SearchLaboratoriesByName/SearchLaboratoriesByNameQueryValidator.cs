using FluentValidation;

namespace WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByName;

public class SearchLaboratoriesByNameQueryValidator : AbstractValidator<SearchLaboratoriesByNameQuery>
{
    private const int MinimumLength = 3;

    public SearchLaboratoriesByNameQueryValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
                .WithMessage("'Name' is required")
            .MinimumLength(MinimumLength)
                .WithMessage(a => $"'Name' minimum length is {MinimumLength}, provided's length is {a.Name.Length} ({a.Name}) ")
            ;
    }
}