using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.DeleteInternationalTaxonomyClassification;

public class DeleteInternationalTaxonomyClassificationCommandValidator : AbstractValidator<DeleteInternationalTaxonomyClassificationCommand>
{
    public DeleteInternationalTaxonomyClassificationCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}