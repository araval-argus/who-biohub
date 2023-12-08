namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubHistoryItemDocument
{
    public Guid? WorklistFromBioHubHistoryItemId { get; set; }
    public WorklistFromBioHubHistoryItem WorklistFromBioHubHistoryItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public bool? IsDocumentFile { get; set; }
}

