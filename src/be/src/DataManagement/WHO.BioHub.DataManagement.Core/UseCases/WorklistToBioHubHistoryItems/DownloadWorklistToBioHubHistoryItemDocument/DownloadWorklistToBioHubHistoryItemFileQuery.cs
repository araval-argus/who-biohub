using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DownloadWorklistToBioHubHistoryItemFile;

public record struct DownloadWorklistToBioHubHistoryItemFileQuery(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    IEnumerable<string> UserPermissions,
    Guid WorklistId
 );