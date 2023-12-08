using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItems;

public record struct ListDashboardWorklistToBioHubItemsQuery(
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    IEnumerable<string> UserPermissions);