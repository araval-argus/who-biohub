using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.WorkflowEngine.Commands;

public record struct MoveToNextStatusToBioHubEngineCommand(
    Guid Id,
    Guid? UserId,
    Guid? OriginalDocumentTemplateSMTA1DocumentId,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile? File,
    FillingOption? Annex2FillingOption,
    IEnumerable<MaterialShippingInformationDto> MaterialShippingInformations,
    IEnumerable<WorklistItemUserDto> WorklistToBioHubItemLaboratoryFocalPoints,
    Guid? Annex2OfSMTA1DocumentId,
    Guid? OriginalDocumentTemplateAnnex2OfSMTA1DocumentId,
    string Annex2OfSMTA1DocumentName,
    Guid? OriginalDocumentTemplateBookingFormOfSMTA1DocumentId,
    Guid? BookingFormOfSMTA1DocumentId,
    string BookingFormOfSMTA1DocumentName,
    Guid? BookingFormOfSMTA1SignatureId,
    string BookingForm2OfSMTA1SignatureString,
    FillingOption? BookingFormFillingOption,
    List<BookingFormOfSMTADto> BookingForms,
    bool IsSaveDraft,
    ShipmentDocumentOperationType? ShipmentDocumentOperationType,
    Guid? ShipmentDocumentId,
    string ShipmentDocumentNewName,
    List<ShipmentDocumentDto> ShipmentDocuments,
    string NewFeedback,
    List<FeedbackDto> feedbacks,
    IEnumerable<WorklistToBioHubItemMaterialDto> WorklistToBioHubItemMaterials,
    IEnumerable<WorklistItemUserDto> WorklistToBioHubItemBioHubFacilityFocalPoints,
    Guid? CurrentDownloadSMTA1DocumentId
);

