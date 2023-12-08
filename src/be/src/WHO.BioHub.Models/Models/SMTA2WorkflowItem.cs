using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class SMTA2WorkflowItem : EntityBase
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
    public virtual ICollection<SMTA2WorkflowHistoryItem> SMTA2WorkflowHistoryItems { get; set; }
    public virtual ICollection<SMTA2WorkflowItemDocument> SMTA2WorkflowItemDocuments { get; set; }
    public bool? IsPast { get; set; }

}



