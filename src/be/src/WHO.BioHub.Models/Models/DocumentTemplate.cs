using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class DocumentTemplate : EntityBase
{

    public string Name { get; set; }
    public string Extension { get; set; }
    public DocumentTemplateType Type { get; set; }
    public DocumentFileType? FileType { get; set; }
    public bool? Current { get; set; }
    public DateTime? UploadTime { get; set; }
    public Guid? UploadedById { get; set; }
    public virtual User UploadedBy { get; set; }
    public Guid? ParentId { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
}
