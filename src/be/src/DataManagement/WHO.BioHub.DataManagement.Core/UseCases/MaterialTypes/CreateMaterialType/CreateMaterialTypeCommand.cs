namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;

public record struct CreateMaterialTypeCommand(
    string Name,
    string Description,
    bool IsActive
);