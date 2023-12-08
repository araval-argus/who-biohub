using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class WorklistFromBioHubItemDto
    {
        public Guid Id { get; set; }
        public WorklistFromBioHubStatus CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public WorklistFromBioHubStatus PreviousStatus { get; set; }
        public string WorklistItemTitle { get; set; }
        public DateTime? OperationDate { get; set; }
        public bool? LastSubmissionApproved { get; set; }
        public string LaboratoryName { get; set; }
        public string LaboratoryAbbreviation { get; set; }
        public string LaboratoryAddress { get; set; }
        public string LaboratoryCountry { get; set; }
        public string BioHubFacilityName { get; set; }
        public string BioHubFacilityCountry { get; set; }
        public string BioHubFacilityAddress { get; set; }
        public string UserName { get; set; }
        public string UserRoleName { get; set; }
        public string UserRoleTypeName { get; set; }
        public RoleType UserRoleType { get; set; }
        public string LastOperationUserBusinessPhone { get; set; }
        public string LastOperationUserMobilePhone { get; set; }
        public string LastOperationUserFirstName { get; set; }
        public string LastOperationUserLastName { get; set; }
        public string LastOperationUserEmail { get; set; }
        public string LastOperationUserJobTitle { get; set; }
        public string Comment { get; set; }
        public Guid? SMTA2DocumentId { get; set; }
        public string SMTA2DocumentName { get; set; }
        public Guid? LaboratoryId { get; set; }
        public Guid? BioHubFacilityId { get; set; }
        public Guid? OriginalDocumentTemplateSMTA2DocumentId { get; set; }
        public bool HistoryDto { get; set; }
        public Guid? OriginalDocumentTemplateAnnex2OfSMTA2DocumentId { get; set; }
        public Guid? Annex2OfSMTA2DocumentId { get; set; }
        public string Annex2OfSMTA2DocumentName { get; set; }
        public Guid? Annex2OfSMTA2SignatureId { get; set; }
        public string Annex2OfSMTA2SignatureString { get; set; }
        public string Annex2Comment { get; set; }
        public bool? Annex2TermsAndConditions { get; set; }
        public FillingOption? Annex2FillingOption { get; set; }
        public IEnumerable<WorklistFromBioHubItemMaterialDto> WorklistFromBioHubItemMaterials { get; set; }
        public IEnumerable<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto> WorklistFromBioHubItemAnnex2OfSMTA2Conditions { get; set; }
        public IEnumerable<WorklistItemUserDto> LaboratoryFocalPoints { get; set; }

        public IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s { get; set; }

        public Guid? OriginalDocumentTemplateBookingFormOfSMTA2DocumentId { get; set; }
        public Guid? BookingFormOfSMTA2DocumentId { get; set; }
        public string BookingFormOfSMTA2DocumentName { get; set; }
        public Guid? BookingFormOfSMTA2SignatureId { get; set; }
        public string BookingFormOfSMTA2SignatureString { get; set; }
        public FillingOption? BookingFormFillingOption { get; set; }
        public IEnumerable<BookingFormOfSMTADto> BookingForms { get; set; }
        public DateTime? BookingFormEstimateDateOfPickup { get; set; }
        public IEnumerable<ShipmentDocumentDto> BHFShipmentDocuments { get; set; }
        public IEnumerable<ShipmentDocumentDto> QEShipmentDocuments { get; set; }
        public Guid? PackagingListDocumentTemplateId { get; set; }
        public string PackagingListDocumentTemplateName { get; set; }
        public Guid? NonCommercialInvoiceCatADocumentTemplateId { get; set; }
        public string NonCommercialInvoiceCatADocumentTemplateName { get; set; }
        public Guid? NonCommercialInvoiceCatBDocumentTemplateId { get; set; }
        public string NonCommercialInvoiceCatBDocumentTemplateName { get; set; }
        public IEnumerable<FeedbackDto> Feedbacks { get; set; }
        public string WHODocumentRegistrationNumber { get; set; }
        public Guid? ReferenceId { get; set; }
        public Guid? OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId { get; set; }
        public Guid? BiosafetyChecklistOfSMTA2DocumentId { get; set; }
        public string BiosafetyChecklistOfSMTA2DocumentName { get; set; }
        public Guid? BiosafetyChecklistOfSMTA2SignatureId { get; set; }
        public string BiosafetyChecklistOfSMTA2SignatureString { get; set; }
        public FillingOption? BiosafetyChecklistFillingOption { get; set; }
        public bool CanSkipSMTA2Phase { get; set; }

        public IEnumerable<BiosafetyChecklistThreadCommentDto> BiosafetyChecklistThreadComments { get; set; }
        public string NewBiosafetyChecklistThreadComment { get; set; }
        public DateTime? PreviousOperationDate { get; set; }
        public Guid? PreviousUserId { get; set; }

        public string PreviousActionBy { get; set; }
        public string NextActionBy { get; set; }
        public string LaboratoryCountryName { get; set; }

        public Guid? CurrentDownloadSMTA2DocumentId { get; set; }
        public string CurrentDownloadSMTA2DocumentName { get; set; }
        public SMTAApprovalStatus SMTA2ApprovalStatus { get; set; }
        public DateTime? SMTA2ApprovalDate { get; set; }
        public bool? IsPast { get; set; }

        //# 54317
        public string Annex2OfSMTA2SignatureText { get; set; }
        public string BookingFormOfSMTA2SignatureText { get; set; }
        public string BiosafetyChecklistOfSMTA2SignatureText { get; set; }
        ///////
    }
}
