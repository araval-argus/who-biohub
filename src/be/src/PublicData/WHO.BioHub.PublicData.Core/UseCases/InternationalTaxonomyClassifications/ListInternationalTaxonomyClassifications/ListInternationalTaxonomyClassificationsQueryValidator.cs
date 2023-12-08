using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;

public class ListInternationalTaxonomyClassificationsQueryValidator : AbstractValidator<ListInternationalTaxonomyClassificationsQuery>
{
    public ListInternationalTaxonomyClassificationsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}