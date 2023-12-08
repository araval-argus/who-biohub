using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class SMTA2WorkflowItemDto
    {
        public Guid Id { get; set; }
        public SMTA2WorkflowStatus CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public SMTA2WorkflowStatus PreviousStatus { get; set; }
        public string WorkflowItemTitle { get; set; }
        public DateTime? OperationDate { get; set; }
        public bool? LastSubmissionApproved { get; set; }
        public string LaboratoryName { get; set; }
        public string LaboratoryAbbreviation { get; set; }
        public string LaboratoryCountry { get; set; }
        public string BioHubFacilityName { get; set; }
        public string UserName { get; set; }
        public string UserRoleName { get; set; }
        public string UserRoleTypeName { get; set; }
        public RoleType UserRoleType { get; set; }
        public string Comment { get; set; }
        public Guid? SMTA2DocumentId { get; set; }
        public string SMTA2DocumentName { get; set; }
        public Guid? LaboratoryId { get; set; }
        public Guid? OriginalDocumentTemplateSMTA2DocumentId { get; set; }
        public bool HistoryDto { get; set; }
        public Guid? ReferenceId { get; set; }
        public bool CanSkipSMTA2Phase { get; set; }
        public string PreviousActionBy { get; set; }
        public string NextActionBy { get; set; }
        public bool? IsPast { get; set; }

    }
}
