using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubItemDocument
{
    public Guid? WorklistToBioHubItemId { get; set; }
    public WorklistToBioHubItem WorklistToBioHubItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public DocumentFileType Type { get; set; }
    public bool? IsDocumentFile { get; set; }
}

