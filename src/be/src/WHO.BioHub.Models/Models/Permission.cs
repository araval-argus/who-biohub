using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class Permission : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}

