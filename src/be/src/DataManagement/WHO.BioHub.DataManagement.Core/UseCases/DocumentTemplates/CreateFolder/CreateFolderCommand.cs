namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CreateFolder;

public record struct CreateFolderCommand(
    Guid? ParentId,
    string Name
);