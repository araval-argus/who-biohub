namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;

public record struct CreateBSLLevelCommand(
    string Name,
    string Description,
    string Code
    );