using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

// This class is used by Entity Framework Core which needs empty constructor

#pragma warning disable CS8618

public class Document : EntityBase
{
    public DateTime OperationDate { get; set; }
    public Guid? LaboratoryId { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public Guid? UploadedById { get; set; }
    public virtual User UploadedBy { get; set; }
    public virtual Laboratory Laboratory { get; set; }
    public DocumentFileType? Type { get; set; }
    public bool? Approved { get; set; }
    public Guid? OriginalDocumentTemplateId { get; set; }
    public bool IsDocumentFile { get; set; }
    public string Base64String { get; set; }
    public virtual DocumentTemplate OriginalDocumentTemplate { get; set; }
    public Guid? BioHubFacilityId { get; set; }
    public BioHubFacility BioHubFacility { get; set; }
    public virtual ICollection<WorklistToBioHubItemDocument> WorklistToBioHubItemDocuments { get; set; }
    public virtual ICollection<WorklistToBioHubHistoryItemDocument> WorklistToBioHubHistoryItemDocuments { get; set; }
    public virtual ICollection<WorklistFromBioHubItemDocument> WorklistFromBioHubItemDocuments { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemDocument> WorklistFromBioHubHistoryItemDocuments { get; set; }
    public virtual ICollection<SMTA1WorkflowItemDocument> SMTA1WorkflowItemDocuments { get; set; }
    public virtual ICollection<SMTA1WorkflowHistoryItemDocument> SMTA1WorkflowHistoryItemDocuments { get; set; }

    public virtual ICollection<SMTA2WorkflowItemDocument> SMTA2WorkflowItemDocuments { get; set; }
    public virtual ICollection<SMTA2WorkflowHistoryItemDocument> SMTA2WorkflowHistoryItemDocuments { get; set; }

}
