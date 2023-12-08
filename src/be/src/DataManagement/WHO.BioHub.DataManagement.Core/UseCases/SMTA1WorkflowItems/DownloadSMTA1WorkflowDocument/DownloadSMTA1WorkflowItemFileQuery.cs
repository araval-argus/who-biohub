using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DownloadSMTA1WorkflowItemFile;

public record struct DownloadSMTA1WorkflowItemFileQuery(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    IEnumerable<string> UserPermissions,
    Guid WorkflowId
 );