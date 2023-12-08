using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class RolePermission
{
    public Guid? RoleId { get; set; }
    public Role Role { get; set; }
    public Guid? PermissionId { get; set; }
    public Permission Permission { get; set; }
}

