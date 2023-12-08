using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.WorkflowEngine.Commands;

public record struct MoveToNextStatusFromBioHubEngineCommand(
    Guid Id,
    Guid? UserId,
    Guid? OriginalDocumentTemplateSMTA2DocumentId,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile? File,
    FillingOption? Annex2FillingOption,
    IEnumerable<WorklistFromBioHubItemMaterialDto> WorklistFromBioHubItemMaterials,
    IEnumerable<WorklistItemUserDto> WorklistFromBioHubItemLaboratoryFocalPoints,
    IEnumerable<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto> WorklistFromBioHubItemAnnex2OfSMTA2Conditions,
    IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s,
    Guid? Annex2OfSMTA2DocumentId,
    Guid? OriginalDocumentTemplateAnnex2OfSMTA2DocumentId,
    string Annex2OfSMTA2DocumentName,
    Guid? OriginalDocumentTemplateBookingFormOfSMTA2DocumentId,
    Guid? BookingFormOfSMTA2DocumentId,
    string BookingFormOfSMTA2DocumentName,
    Guid? BookingFormOfSMTA2SignatureId,
    string BookingForm2OfSMTA2SignatureString,
    FillingOption? BookingFormFillingOption,
    List<BookingFormOfSMTADto> BookingForms,
    bool IsSaveDraft,
    ShipmentDocumentOperationType? ShipmentDocumentOperationType,
    Guid? ShipmentDocumentId,
    string ShipmentDocumentNewName,
    List<ShipmentDocumentDto> BHFShipmentDocuments,
    List<ShipmentDocumentDto> QEShipmentDocuments,
    string NewFeedback,
    List<FeedbackDto> Feedbacks,
    FillingOption? BiosafetyChecklistFillingOption,
    string NewBiosafetyChecklistThreadComment,
    List<BiosafetyChecklistThreadCommentDto> BiosafetyChecklistThreadComments,
    string NewBiosafetyChecklistThreadCommentFromDocument,
    DateTime? PreviousOperationDate,
    Guid? PreviousUserId,
    Guid? CurrentDownloadSMTA2DocumentId
);

