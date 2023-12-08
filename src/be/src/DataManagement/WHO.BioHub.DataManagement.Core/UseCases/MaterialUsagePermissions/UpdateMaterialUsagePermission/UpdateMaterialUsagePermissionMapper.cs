using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;

public interface IUpdateMaterialUsagePermissionMapper
{
    MaterialUsagePermission Map(MaterialUsagePermission materialusagepermission, UpdateMaterialUsagePermissionCommand command);
}

public class UpdateMaterialUsagePermissionMapper : IUpdateMaterialUsagePermissionMapper
{
    public MaterialUsagePermission Map(MaterialUsagePermission materialusagepermission, UpdateMaterialUsagePermissionCommand command)
    {            

        materialusagepermission.Id = command.Id;
        materialusagepermission.Name = command.Name;
        materialusagepermission.Description = command.Description;
        materialusagepermission.IsActive = command.IsActive;
        materialusagepermission.DeletedOn = null;

        return materialusagepermission;
    }
}