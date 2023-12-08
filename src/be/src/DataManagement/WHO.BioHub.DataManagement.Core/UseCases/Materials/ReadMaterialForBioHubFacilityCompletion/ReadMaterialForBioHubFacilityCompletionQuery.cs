using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForBioHubFacilityCompletion;

public record struct ReadMaterialForBioHubFacilityCompletionQuery(
    Guid Id,
    RoleType RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId
    );