using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.UpdateInternationalTaxonomyClassification;

public class UpdateInternationalTaxonomyClassificationCommandValidator : AbstractValidator<UpdateInternationalTaxonomyClassificationCommand>
{
    public UpdateInternationalTaxonomyClassificationCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}