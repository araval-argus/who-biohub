using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public record struct ListMaterialsQuery(
    RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId);