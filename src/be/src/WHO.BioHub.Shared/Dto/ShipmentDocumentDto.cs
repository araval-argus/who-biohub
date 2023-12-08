using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class ShipmentDocumentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DocumentFileType? FileType { get; set; }
        public DateTime? UploadTime { get; set; }
        public Guid? UploadedById { get; set; }
        public string UploadedBy { get; set; }
        public RoleType? UploaderRoleType { get; set; }
    }
}
