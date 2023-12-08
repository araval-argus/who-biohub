using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterial;

public record struct ReadMaterialQuery(
    Guid Id,
    RoleType RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId
    );