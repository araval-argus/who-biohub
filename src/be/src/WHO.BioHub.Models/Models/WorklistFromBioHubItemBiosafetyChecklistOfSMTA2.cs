using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 : EntityBase
{
    public Guid? WorklistFromBioHubItemId { get; set; }
    public WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
    public Guid? BiosafetyChecklistOfSMTA2Id { get; set; }
    public BiosafetyChecklistOfSMTA2 BiosafetyChecklistOfSMTA2 { get; set; }
    public bool? Flag { get; set; }
}

