using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ListWorklistToBioHubHistoryItems;

public record struct ListWorklistToBioHubHistoryItemsQuery(
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    Guid WorlistToBioHubItemId,
    IEnumerable<string> UserPermissions
    );