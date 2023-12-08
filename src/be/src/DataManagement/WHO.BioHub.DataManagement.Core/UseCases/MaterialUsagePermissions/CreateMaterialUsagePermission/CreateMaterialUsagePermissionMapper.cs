using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;

public interface ICreateMaterialUsagePermissionMapper
{
    MaterialUsagePermission Map(CreateMaterialUsagePermissionCommand command);
}

public class CreateMaterialUsagePermissionMapper : ICreateMaterialUsagePermissionMapper
{
    public MaterialUsagePermission Map(CreateMaterialUsagePermissionCommand command)
    {      

        MaterialUsagePermission materialusagepermission = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return materialusagepermission;
    }
}