namespace WHO.BioHub.Shared.Enums
{
    public class DocumentItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DocumentItemType Type { get; set; }
        public DocumentFileType? FileType { get; set; }
        public DateTime? UploadTime { get; set; }
        public Guid? UploadedById { get; set; }
        public string UploadedBy { get; set; }
        public Guid? ParentId { get; set; }
        public List<DocumentItemDto> SubFolders { get; set; }
    }
}
