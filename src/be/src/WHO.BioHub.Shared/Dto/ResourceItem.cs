using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class ResourceItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }       
        public DateTime? UploadTime { get; set; }
        public Guid? UploadedById { get; set; }
        public string UploadedBy { get; set; }
        public ResourceType Type { get; set; }
        public ResourceFileType? FileType { get; set; }
        public bool? Current { get; set; }       
        public Guid? ParentId { get; set; }
    }
}
