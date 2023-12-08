namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;

public record struct UpdateBSLLevelCommand(
    Guid Id,
    string Name,
    string Description,
    string Code
    );