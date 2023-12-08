using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class SMTA2WorkflowItemDocument
{
    public Guid? SMTA2WorkflowItemId { get; set; }
    public SMTA2WorkflowItem SMTA2WorkflowItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public DocumentFileType Type { get; set; }
    public bool? IsDocumentFile { get; set; }
}

