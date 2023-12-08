using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class SMTA2WorkflowHistoryItem : EntityBase
{
    public SMTA2WorkflowStatus Status { get; set; }
    public SMTA2WorkflowStatus PreviousStatus { get; set; }
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
    public Guid? SMTA2WorkflowItemId { get; set; }
    public virtual SMTA2WorkflowItem SMTA2WorkflowItem { get; set; }
    public virtual ICollection<SMTA2WorkflowHistoryItemDocument> SMTA2WorkflowHistoryItemDocuments { get; set; }
    public bool? IsPast { get; set; }
}



