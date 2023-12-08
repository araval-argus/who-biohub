using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.CreateInternationalTaxonomyClassification;

public class CreateInternationalTaxonomyClassificationCommandValidator : AbstractValidator<CreateInternationalTaxonomyClassificationCommand>
{
    public CreateInternationalTaxonomyClassificationCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}