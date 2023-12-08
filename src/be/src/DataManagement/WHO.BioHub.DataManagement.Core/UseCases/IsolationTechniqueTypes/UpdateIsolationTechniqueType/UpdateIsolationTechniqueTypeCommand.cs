namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;

public record struct UpdateIsolationTechniqueTypeCommand(Guid Id,
    string Name,
    string Description,
    bool IsActive);