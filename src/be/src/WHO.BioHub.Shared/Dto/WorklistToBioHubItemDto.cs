using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class WorklistToBioHubItemDto
    {
        public Guid Id { get; set; }
        public WorklistToBioHubStatus CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public WorklistToBioHubStatus PreviousStatus { get; set; }
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
        public Guid? SMTA1DocumentId { get; set; }
        public string SMTA1DocumentName { get; set; }
        public Guid? LaboratoryId { get; set; }
        public Guid? BioHubFacilityId { get; set; }
        public Guid? OriginalDocumentTemplateSMTA1DocumentId { get; set; }
        public bool HistoryDto { get; set; }
        public Guid? OriginalDocumentTemplateAnnex2OfSMTA1DocumentId { get; set; }
        public Guid? Annex2OfSMTA1DocumentId { get; set; }
        public string Annex2OfSMTA1DocumentName { get; set; }
        public Guid? Annex2OfSMTA1SignatureId { get; set; }
        public string Annex2OfSMTA1SignatureString { get; set; }
        public string Annex2Comment { get; set; }
        public bool? Annex2TermsAndConditions { get; set; }
        public FillingOption? Annex2FillingOption { get; set; }
        public IEnumerable<MaterialShippingInformationDto> MaterialShippingInformations { get; set; }
        public IEnumerable<WorklistItemUserDto> LaboratoryFocalPoints { get; set; }
        public Guid? OriginalDocumentTemplateBookingFormOfSMTA1DocumentId { get; set; }
        public Guid? BookingFormOfSMTA1DocumentId { get; set; }
        public string BookingFormOfSMTA1DocumentName { get; set; }
        public Guid? BookingFormOfSMTA1SignatureId { get; set; }
        public string BookingFormOfSMTA1SignatureString { get; set; }
        public FillingOption? BookingFormFillingOption { get; set; }
        public IEnumerable<BookingFormOfSMTADto> BookingForms { get; set; }
        public DateTime? BookingFormEstimateDateOfPickup { get; set; }
        public IEnumerable<ShipmentDocumentDto> ShipmentDocuments { get; set; }
        public Guid? PackagingListDocumentTemplateId { get; set; }
        public string PackagingListDocumentTemplateName { get; set; }
        public Guid? NonCommercialInvoiceCatADocumentTemplateId { get; set; }
        public string NonCommercialInvoiceCatADocumentTemplateName { get; set; }
        public Guid? NonCommercialInvoiceCatBDocumentTemplateId { get; set; }
        public string NonCommercialInvoiceCatBDocumentTemplateName { get; set; }
        public IEnumerable<FeedbackDto> Feedbacks { get; set; }
        public string WHODocumentRegistrationNumber { get; set; }
        public Guid? ReferenceId { get; set; }
        public IEnumerable<WorklistToBioHubItemMaterialDto> WorklistToBioHubItemMaterials { get; set; }
        public bool CanSkipSMTA1Phase { get; set; }
        public IEnumerable<WorklistItemUserDto> WorklistToBioHubItemBioHubFacilityFocalPoints { get; set; }

        public string PreviousActionBy { get; set; }
        public string NextActionBy { get; set; }
        public string LaboratoryCountryName { get; set; }
        public Guid? CurrentDownloadSMTA1DocumentId { get; set; }
        public string CurrentDownloadSMTA1DocumentName { get; set; }
        public SMTAApprovalStatus SMTA1ApprovalStatus { get; set; }
        public DateTime? SMTA1ApprovalDate { get; set; }
        public bool? IsPast { get; set; }

        //# 54317
        public string Annex2OfSMTA1SignatureText { get; set; }
        public string BookingFormOfSMTA1SignatureText { get; set; }
        ///////

    }
}
