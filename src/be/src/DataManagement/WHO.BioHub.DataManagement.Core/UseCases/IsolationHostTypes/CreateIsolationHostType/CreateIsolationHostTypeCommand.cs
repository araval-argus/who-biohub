namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;

public record struct CreateIsolationHostTypeCommand(
    string Name,
    string Description,
    bool IsActive
);