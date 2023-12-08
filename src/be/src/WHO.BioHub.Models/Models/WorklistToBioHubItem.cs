using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubItem : EntityBase
{
    public WorklistToBioHubStatus Status { get; set; }
    public WorklistToBioHubStatus PreviousStatus { get; set; }
    public string WorklistItemTitle { get; set; }
    public DateTime? OperationDate { get; set; }
    public bool Completed { get; set; }
    public Guid? RequestInitiationFromLaboratoryId { get; set; }
    public virtual Laboratory RequestInitiationFromLaboratory { get; set; }
    public Guid? RequestInitiationToBioHubFacilityId { get; set; }
    public virtual BioHubFacility RequestInitiationToBioHubFacility { get; set; }
    public Guid? LastOperationUserId { get; set; }
    public virtual User LastOperationUser { get; set; }
    public bool? LastSubmissionApproved { get; set; }
    public string Comment { get; set; }
    public virtual ICollection<WorklistToBioHubHistoryItem> WorklistToBioHubHistoryItems { get; set; }
    public virtual ICollection<WorklistToBioHubItemDocument> WorklistToBioHubItemDocuments { get; set; }
    public virtual ICollection<MaterialShippingInformation> MaterialShippingInformations { get; set; }
    public virtual ICollection<WorklistToBioHubItemLaboratoryFocalPoint> WorklistToBioHubItemLaboratoryFocalPoints { get; set; }
    public string Annex2Comment { get; set; }
    public bool? Annex2TermsAndConditions { get; set; }
    public FillingOption? Annex2FillingOption { get; set; }
    public bool? Annex2ApprovalFlag { get; set; }
    public string Annex2ApprovalComment { get; set; }
    public DateTime? Annex2OfSMTA1ApprovalDate { get; set; }

    public Guid? OriginalAnnex2OfSMTA1DocumentTemplateId { get; set; }
    public virtual ICollection<BookingForm> BookingForms { get; set; }
    public FillingOption? BookingFormFillingOption { get; set; }
    public bool? BookingFormApprovalFlag { get; set; }
    public string BookingFormApprovalComment { get; set; }
    public DateTime? BookingFormOfSMTA1ApprovalDate { get; set; }
    public Guid? OriginalBookingFormOfSMTA1DocumentTemplateId { get; set; }
    public bool? WaitForArrivalConditionCheckApprovalFlag { get; set; }
    public string WaitForArrivalConditionCheckApprovalComment { get; set; }
    public virtual ICollection<WorklistToBioHubItemFeedback> WorklistToBioHubItemFeedbacks { get; set; }
    public string WHODocumentRegistrationNumber { get; set; }
    public string ReferenceNumber { get; set; }
    public virtual ICollection<Shipment> Shipments { get; set; }
    public Guid? ReferenceId { get; set; }
    public virtual ICollection<WorklistToBioHubItemMaterial> WorklistToBioHubItemMaterials { get; set; }
    public virtual ICollection<WorklistToBioHubItemBioHubFacilityFocalPoint> WorklistToBioHubItemBioHubFacilityFocalPoints { get; set; }
    public bool? IsPast { get; set; }

    //# 54317
    public string Annex2OfSMTA1SignatureText { get; set; }
    public string BookingFormOfSMTA1SignatureText { get; set; }
    ///////
}

