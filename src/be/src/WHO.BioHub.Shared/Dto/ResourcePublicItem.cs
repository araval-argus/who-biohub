using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class ResourcePublicItem
    {
        public Guid Id { get; set; }
        public string FileCompleteName { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public ResourceFileType? FileType { get; set; }       
        public Guid? ParentId { get; set; }
    }
}

