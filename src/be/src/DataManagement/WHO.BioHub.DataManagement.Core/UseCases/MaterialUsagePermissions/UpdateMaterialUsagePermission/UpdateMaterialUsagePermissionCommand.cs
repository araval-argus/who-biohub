namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;

public record struct UpdateMaterialUsagePermissionCommand(Guid Id,
    string Name,
    string Description,
    bool IsActive);