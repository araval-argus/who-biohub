using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ReadInternationalTaxonomyClassification;

public class ReadInternationalTaxonomyClassificationQueryValidator : AbstractValidator<ReadInternationalTaxonomyClassificationQuery>
{
    public ReadInternationalTaxonomyClassificationQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}