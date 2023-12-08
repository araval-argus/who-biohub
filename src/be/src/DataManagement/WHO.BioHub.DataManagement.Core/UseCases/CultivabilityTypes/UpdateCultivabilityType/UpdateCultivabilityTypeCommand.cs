namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.UpdateCultivabilityType;

public record struct UpdateCultivabilityTypeCommand(Guid Id,
    string Name,
    string Description,
    bool IsActive);