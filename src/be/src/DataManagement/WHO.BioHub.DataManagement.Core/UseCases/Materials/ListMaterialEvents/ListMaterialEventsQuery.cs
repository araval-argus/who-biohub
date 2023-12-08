using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialEvents.ListMaterialEvents;

public record struct ListMaterialEventsQuery(
    Guid Id,
    RoleType RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId);