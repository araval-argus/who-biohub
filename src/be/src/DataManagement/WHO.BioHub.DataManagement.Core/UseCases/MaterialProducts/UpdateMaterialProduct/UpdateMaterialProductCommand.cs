namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.UpdateMaterialProduct;

public record struct UpdateMaterialProductCommand(Guid Id,
    string Name,
    string Description,
    bool IsActive);