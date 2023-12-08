namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateFolder;

public record struct CreateFolderCommand(
    Guid? ParentId,
    string Name
);