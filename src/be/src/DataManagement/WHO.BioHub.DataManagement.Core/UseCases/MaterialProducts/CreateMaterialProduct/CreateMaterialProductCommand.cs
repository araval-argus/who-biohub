namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;

public record struct CreateMaterialProductCommand(
    string Name,
    string Description,
    bool IsActive
);