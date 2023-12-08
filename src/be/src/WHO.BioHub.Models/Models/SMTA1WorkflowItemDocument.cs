using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class SMTA1WorkflowItemDocument
{
    public Guid? SMTA1WorkflowItemId { get; set; }
    public SMTA1WorkflowItem SMTA1WorkflowItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public DocumentFileType Type { get; set; }
    public bool? IsDocumentFile { get; set; }
}

