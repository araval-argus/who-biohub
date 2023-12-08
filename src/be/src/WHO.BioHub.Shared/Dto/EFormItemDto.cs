using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class EFormItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public EFormItemType Type { get; set; }
        public EFormType? EFormType { get; set; }
        public DateTime? ApprovedTime { get; set; }
        public Guid? ParentId { get; set; }
        public List<EFormItemDto> SubFolders { get; set; }
    }
}
