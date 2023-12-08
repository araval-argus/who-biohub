using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubItemDocument
{
    public Guid? WorklistFromBioHubItemId { get; set; }
    public WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
    public Guid? DocumentId { get; set; }
    public Document Document { get; set; }
    public DocumentFileType Type { get; set; }
    public bool? IsDocumentFile { get; set; }
}

