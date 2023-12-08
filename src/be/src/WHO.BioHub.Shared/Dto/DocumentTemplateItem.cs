using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class DocumentTemplateItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DocumentTemplateType Type { get; set; }
        public DocumentFileType? FileType { get; set; }
        public bool? Current { get; set; }
        public DateTime? UploadTime { get; set; }
        public Guid? UploadedById { get; set; }
        public string UploadedBy { get; set; }
        public Guid? ParentId { get; set; }
    }
}
