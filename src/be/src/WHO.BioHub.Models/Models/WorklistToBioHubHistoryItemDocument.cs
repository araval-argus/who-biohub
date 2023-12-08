namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubHistoryItemDocument
{
    public Guid? WorklistToBioHubHistoryItemId { get; set; }
    public WorklistToBioHubHistoryItem WorklistToBioHubHistoryItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public bool? IsDocumentFile { get; set; }
}

