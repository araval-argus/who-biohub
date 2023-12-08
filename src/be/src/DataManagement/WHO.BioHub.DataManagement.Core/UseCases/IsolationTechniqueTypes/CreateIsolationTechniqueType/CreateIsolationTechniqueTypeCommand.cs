namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;

public record struct CreateIsolationTechniqueTypeCommand(
    string Name,
    string Description,
    bool IsActive
);