using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.InternationalTaxonomyClassifications.ReadInternationalTaxonomyClassification;

public class ReadInternationalTaxonomyClassificationQueryValidator : AbstractValidator<ReadInternationalTaxonomyClassificationQuery>
{
    public ReadInternationalTaxonomyClassificationQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}