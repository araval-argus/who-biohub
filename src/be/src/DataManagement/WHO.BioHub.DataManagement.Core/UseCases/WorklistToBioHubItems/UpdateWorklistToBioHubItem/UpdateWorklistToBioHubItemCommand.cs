using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;

public record struct UpdateWorklistToBioHubItemCommand(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    WorklistToBioHubStatus CurrentStatus,
    Guid? UserId,
    bool? LastSubmissionApproved,
    string Comment,
    Guid? OriginalDocumentTemplateSMTA1DocumentId,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile? File,
    IEnumerable<string> UserPermissions,
    string Annex2Comment,
    bool? Annex2TermsAndConditions,
    FillingOption? Annex2FillingOption,
    IEnumerable<MaterialShippingInformationDto> MaterialShippingInformations,
    IEnumerable<WorklistItemUserDto> LaboratoryFocalPoints,
    bool? Annex2ApprovalFlag,
    string Annex2ApprovalComment,
    Guid? Annex2OfSMTA1DocumentId,
    Guid? OriginalDocumentTemplateAnnex2OfSMTA1DocumentId,
    string Annex2OfSMTA1DocumentName,
    Guid? Annex2OfSMTA1SignatureId,
    Guid? OriginalDocumentTemplateBookingFormOfSMTA1DocumentId,
    FillingOption? BookingFormFillingOption,
    Guid? BookingFormOfSMTA1DocumentId,
    string BookingFormOfSMTA1DocumentName,
    Guid? BookingFormOfSMTA1SignatureId,
    string BookingForm2OfSMTA1SignatureString,
    List<BookingFormOfSMTADto> BookingForms,
    bool? BookingFormApprovalFlag,
    string BookingFormApprovalComment,
    bool IsSaveDraft,
    DateTime? BookingFormEstimateDateOfPickup,
    ShipmentDocumentOperationType? ShipmentDocumentOperationType,
    Guid? ShipmentDocumentId,
    string ShipmentDocumentNewName,
    List<ShipmentDocumentDto> ShipmentDocuments,
    bool? WaitForArrivalConditionCheckApprovalFlag,
    string WaitForArrivalConditionCheckApprovalComment,
    string NewFeedback,
    List<FeedbackDto> feedbacks,
    string WHODocumentRegistrationNumber,
    Guid? ReferenceId,
    IEnumerable<WorklistToBioHubItemMaterialDto> WorklistToBioHubItemMaterials,
    IEnumerable<WorklistItemUserDto> WorklistToBioHubItemBioHubFacilityFocalPoints,
    Guid? BioHubFacilityId,
    Guid? LaboratoryId,
    Guid? CurrentDownloadSMTA1DocumentId,
    bool? IsPast,
    DateTime? AssignedOperationDate,

    //# 54317
    string Annex2OfSMTA1SignatureText,
    string BookingFormOfSMTA1SignatureText
///////
);

