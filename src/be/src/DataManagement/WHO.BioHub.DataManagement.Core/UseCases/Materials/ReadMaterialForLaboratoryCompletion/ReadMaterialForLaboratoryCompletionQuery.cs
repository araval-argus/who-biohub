using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForLaboratoryCompletion;

public record struct ReadMaterialForLaboratoryCompletionQuery(
    Guid Id,
    RoleType RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId
    );