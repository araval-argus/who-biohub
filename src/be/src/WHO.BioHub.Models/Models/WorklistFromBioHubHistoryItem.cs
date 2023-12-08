using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubHistoryItem : EntityBase
{
    public WorklistFromBioHubStatus Status { get; set; }
    public WorklistFromBioHubStatus PreviousStatus { get; set; }
    public string WorklistItemTitle { get; set; }
    public DateTime? OperationDate { get; set; }
    public bool Completed { get; set; }
    public Guid? RequestInitiationToLaboratoryId { get; set; }
    public virtual Laboratory RequestInitiationToLaboratory { get; set; }
    public Guid? RequestInitiationFromBioHubFacilityId { get; set; }
    public virtual BioHubFacility RequestInitiationFromBioHubFacility { get; set; }
    public Guid? LastOperationUserId { get; set; }
    public virtual User LastOperationUser { get; set; }
    public bool? LastSubmissionApproved { get; set; }
    public string Comment { get; set; }
    public Guid? WorklistFromBioHubItemId { get; set; }
    public virtual WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemDocument> WorklistFromBioHubHistoryItemDocuments { get; set; }
    public virtual ICollection<MaterialShippingInformationHistory> MaterialShippingInformationsHistory { get; set; }
    public string Annex2Comment { get; set; }
    public bool? Annex2TermsAndConditions { get; set; }
    public FillingOption? Annex2FillingOption { get; set; }
    public bool? Annex2ApprovalFlag { get; set; }
    public string Annex2ApprovalComment { get; set; }
    public DateTime? Annex2OfSMTA2ApprovalDate { get; set; }
    public Guid? OriginalAnnex2OfSMTA2DocumentTemplateId { get; set; }
    public FillingOption? BiosafetyChecklistFillingOption { get; set; }
    public bool? BiosafetyChecklistApprovalFlag { get; set; }
    public string BiosafetyChecklistApprovalComment { get; set; }
    public DateTime? BiosafetyChecklistApprovalDate { get; set; }
    public Guid? OriginalBiosafetyChecklistDocumentTemplateId { get; set; }
    public virtual ICollection<BookingFormHistory> BookingFormsHistory { get; set; }
    public FillingOption? BookingFormFillingOption { get; set; }
    public bool? BookingFormApprovalFlag { get; set; }
    public string BookingFormApprovalComment { get; set; }
    public DateTime? BookingFormOfSMTA2ApprovalDate { get; set; }
    public Guid? OriginalBookingFormOfSMTA2DocumentTemplateId { get; set; }
    public bool? WaitForArrivalConditionCheckApprovalFlag { get; set; }
    public string WaitForArrivalConditionCheckApprovalComment { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemFeedback> WorklistFromBioHubHistoryItemFeedbacks { get; set; }
    public string WHODocumentRegistrationNumber { get; set; }
    public string ReferenceNumber { get; set; }
    public virtual ICollection<Shipment> Shipments { get; set; }
    public Guid? ReferenceId { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition> WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemMaterial> WorklistFromBioHubHistoryItemMaterials { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemLaboratoryFocalPoint> WorklistFromBioHubHistoryItemLaboratoryFocalPoints { get; set; }
    public virtual ICollection<WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComment> WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2Comments { get; set; }
    public string SavedBiosafetyChecklistThreadComment { get; set; }
    public bool? IsPast { get; set; }

    //# 54317
    public string Annex2OfSMTA2SignatureText { get; set; }
    public string BookingFormOfSMTA2SignatureText { get; set; }
    public string BiosafetyChecklistOfSMTA2SignatureText { get; set; }
    ///////
}



