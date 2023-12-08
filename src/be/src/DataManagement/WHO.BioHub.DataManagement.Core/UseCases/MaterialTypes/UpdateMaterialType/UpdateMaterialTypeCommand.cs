namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;

public record struct UpdateMaterialTypeCommand(Guid Id,
    string Name,
    string Description,
    bool IsActive);