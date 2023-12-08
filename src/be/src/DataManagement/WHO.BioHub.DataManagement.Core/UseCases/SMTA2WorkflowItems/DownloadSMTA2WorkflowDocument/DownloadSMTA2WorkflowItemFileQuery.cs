using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DownloadSMTA2WorkflowItemFile;

public record struct DownloadSMTA2WorkflowItemFileQuery(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    IEnumerable<string> UserPermissions,
    Guid WorkflowId
 );