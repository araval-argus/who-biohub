using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class IsolationTechniqueType : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
    public virtual ICollection<MaterialHistory> MaterialsHistory { get; set; }
}
