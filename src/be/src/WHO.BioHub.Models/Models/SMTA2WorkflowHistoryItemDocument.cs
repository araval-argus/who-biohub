namespace WHO.BioHub.Models.Models;

public class SMTA2WorkflowHistoryItemDocument
{
    public Guid? SMTA2WorkflowHistoryItemId { get; set; }
    public SMTA2WorkflowHistoryItem SMTA2WorkflowHistoryItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public bool? IsDocumentFile { get; set; }
}

