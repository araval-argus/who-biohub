using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class SMTA1WorkflowHistoryItem : EntityBase
{
    public SMTA1WorkflowStatus Status { get; set; }
    public SMTA1WorkflowStatus PreviousStatus { get; set; }
    public string WorkflowItemTitle { get; set; }
    public DateTime? OperationDate { get; set; }
    public bool Completed { get; set; }
    public Guid? LaboratoryId { get; set; }
    public virtual Laboratory Laboratory { get; set; }
    public Guid? LastOperationUserId { get; set; }
    public virtual User LastOperationUser { get; set; }
    public bool? LastSubmissionApproved { get; set; }
    public string Comment { get; set; }
    public Guid? ReferenceId { get; set; }
    public Guid? SMTA1WorkflowItemId { get; set; }
    public virtual SMTA1WorkflowItem SMTA1WorkflowItem { get; set; }
    public virtual ICollection<SMTA1WorkflowHistoryItemDocument> SMTA1WorkflowHistoryItemDocuments { get; set; }
    public bool? IsPast { get; set; }

}



