using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DownloadWorklistFromBioHubHistoryItemFile;

public record struct DownloadWorklistFromBioHubHistoryItemFileQuery(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    IEnumerable<string> UserPermissions,
    Guid WorklistId
 );