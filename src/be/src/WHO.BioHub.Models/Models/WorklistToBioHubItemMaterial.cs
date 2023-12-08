using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubItemMaterial : EntityBase
{
    public Guid? WorklistToBioHubItemId { get; set; }
    public WorklistToBioHubItem WorklistToBioHubItem { get; set; }
    public Guid? MaterialId { get; set; }
    public Material Material { get; set; }
}

