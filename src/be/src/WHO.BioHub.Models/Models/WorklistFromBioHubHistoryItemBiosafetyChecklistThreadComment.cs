using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComment : EntityBase
{
    public string Text { get; set; }
    public DateTime? Date { get; set; }
    public Guid? PostedById { get; set; }
    public User PostedBy { get; set; }
    public Guid? WorklistFromBioHubHistoryItemId { get; set; }
    public WorklistFromBioHubHistoryItem WorklistFromBioHubHistoryItem { get; set; }
}

