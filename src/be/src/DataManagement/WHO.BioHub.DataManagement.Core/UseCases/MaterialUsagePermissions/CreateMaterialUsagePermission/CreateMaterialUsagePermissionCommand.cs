namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;

public record struct CreateMaterialUsagePermissionCommand(
    string Name,
    string Description,
    bool IsActive
);