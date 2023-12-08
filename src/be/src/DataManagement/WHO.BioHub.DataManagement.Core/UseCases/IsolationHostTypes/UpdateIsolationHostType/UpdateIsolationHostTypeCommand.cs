namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.UpdateIsolationHostType;

public record struct UpdateIsolationHostTypeCommand(Guid Id,
    string Name,
    string Description,
    bool IsActive);