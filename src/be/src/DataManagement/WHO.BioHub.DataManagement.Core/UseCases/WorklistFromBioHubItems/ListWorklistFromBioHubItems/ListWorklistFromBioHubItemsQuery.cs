using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;

public record struct ListWorklistFromBioHubItemsQuery(
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    IEnumerable<string> UserPermissions);