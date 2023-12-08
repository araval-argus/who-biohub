using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubHistoryItemMaterial : EntityBase
{
    public Guid? WorklistFromBioHubHistoryItemId { get; set; }
    public WorklistFromBioHubHistoryItem WorklistFromBioHubHistoryItem { get; set; }
    public Guid? MaterialId { get; set; }
    public Material Material { get; set; }
    public int? Quantity { get; set; }
    public double? Amount { get; set; }
    public ShipmentMaterialCondition? Condition { get; set; }
    public string Note { get; set; }
}

