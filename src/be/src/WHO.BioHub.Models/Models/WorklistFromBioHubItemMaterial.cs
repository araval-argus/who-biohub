using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubItemMaterial : EntityBase
{
    public Guid? WorklistFromBioHubItemId { get; set; }
    public WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
    public Guid? MaterialId { get; set; }
    public Material Material { get; set; }
    public int? Quantity { get; set; }
    public double? Amount { get; set; }
    public ShipmentMaterialCondition? Condition { get; set; }
    public string Note { get; set; }
}

