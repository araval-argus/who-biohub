using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto
    {
        public Guid Id { get; set; }
        public Guid? Annex2OfSMTA2ConditionId { get; set; }
        public int Order { get; set; }
        public string PointNumber { get; set; }
        public string Condition { get; set; }
        public bool Mandatory { get; set; }
        public bool Selectable { get; set; }
        public bool? Flag { get; set; }
        public string Comment { get; set; }
    }
}
