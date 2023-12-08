using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class Resource : EntityBase
{
    public string Name { get; set; }
    public string Extension { get; set; }    
    public DateTime? UploadTime { get; set; }
    public Guid? UploadedById { get; set; }
    public virtual User UploadedBy { get; set; }    
    public ResourceType Type { get; set; }
    public ResourceFileType? FileType { get; set; }
    public bool? Current { get; set; }    
    public Guid? ParentId { get; set; }   
}
