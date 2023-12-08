using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class MaterialGSDInfoDto
    {
        public Guid? Id { get; set; }        
        public GSDType? GSDType { get; set; }
        public string CellLine { get; set; }
        public int? PassageNumber { get; set; }
        public string GSDFasta { get; set; }
    }
}
