using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ListWorklistFromBioHubHistoryItems;

public record struct ListWorklistFromBioHubHistoryItemsQuery(
    Guid WorlistFromBioHubItemId,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    IEnumerable<string> UserPermissions
    );