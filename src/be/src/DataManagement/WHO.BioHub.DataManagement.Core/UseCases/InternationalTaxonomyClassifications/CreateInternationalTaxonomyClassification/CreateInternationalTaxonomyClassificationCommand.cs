namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.CreateInternationalTaxonomyClassification;

public record struct CreateInternationalTaxonomyClassificationCommand(
    string Name,
    string Description,
    bool IsActive
);