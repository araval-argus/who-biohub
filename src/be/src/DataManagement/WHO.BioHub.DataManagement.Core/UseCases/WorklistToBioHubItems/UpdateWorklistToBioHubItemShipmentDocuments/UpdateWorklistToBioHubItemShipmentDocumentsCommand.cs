using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItemShipmentDocuments;

public record struct UpdateWorklistToBioHubItemShipmentDocumentsCommand(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    WorklistFromBioHubStatus CurrentStatus,
    Guid? UserId,
    ShipmentDocumentOperationType? ShipmentDocumentOperationType,
    DocumentFileType? DocumentTemplateFileType,
    Guid? ShipmentDocumentId,
    string ShipmentDocumentNewName,
    IFormFile? File,
    IEnumerable<string> UserPermissions,
    List<ShipmentDocumentDto> ShipmentDocuments,
    Guid? ReferenceId,
    Guid? BioHubFacilityId,
    Guid? LaboratoryId
);

