namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.CreateCultivabilityType;

public record struct CreateCultivabilityTypeCommand(
    string Name,
    string Description,
    bool IsActive
);