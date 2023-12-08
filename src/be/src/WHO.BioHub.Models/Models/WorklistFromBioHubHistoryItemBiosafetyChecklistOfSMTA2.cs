using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2 : EntityBase
{
    public Guid? WorklistFromBioHubHistoryItemId { get; set; }
    public WorklistFromBioHubHistoryItem WorklistFromBioHubHistoryItem { get; set; }
    public Guid? BiosafetyChecklistOfSMTA2Id { get; set; }
    public BiosafetyChecklistOfSMTA2 BiosafetyChecklistOfSMTA2 { get; set; }
    public bool? Flag { get; set; }
}

