using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemQEShipmentDocuments;

public record struct UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand(
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
    List<ShipmentDocumentDto> QEShipmentDocuments,   
    Guid? ReferenceId,
    Guid? BioHubFacilityId,
    Guid? LaboratoryId
);

