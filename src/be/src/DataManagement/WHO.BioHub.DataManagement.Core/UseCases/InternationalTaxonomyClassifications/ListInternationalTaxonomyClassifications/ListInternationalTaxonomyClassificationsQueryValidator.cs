using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;

public class ListInternationalTaxonomyClassificationsQueryValidator : AbstractValidator<ListInternationalTaxonomyClassificationsQuery>
{
    public ListInternationalTaxonomyClassificationsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}