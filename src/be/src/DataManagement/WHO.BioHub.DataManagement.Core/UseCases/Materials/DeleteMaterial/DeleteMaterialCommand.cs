using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.DeleteMaterial;

public record struct DeleteMaterialCommand(
    Guid Id,
    RoleType RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    Guid? OperationById);