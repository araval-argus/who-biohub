namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.UpdateInternationalTaxonomyClassification;

public record struct UpdateInternationalTaxonomyClassificationCommand(Guid Id,
    string Name,
    string Description,
    bool IsActive);