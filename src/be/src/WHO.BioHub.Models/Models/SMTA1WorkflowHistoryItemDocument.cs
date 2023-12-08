namespace WHO.BioHub.Models.Models;

public class SMTA1WorkflowHistoryItemDocument
{
    public Guid? SMTA1WorkflowHistoryItemId { get; set; }
    public SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public bool? IsDocumentFile { get; set; }
}

