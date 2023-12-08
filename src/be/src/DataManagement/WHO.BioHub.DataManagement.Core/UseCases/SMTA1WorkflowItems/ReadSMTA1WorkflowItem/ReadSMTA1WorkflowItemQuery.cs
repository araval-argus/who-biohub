using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;

public record struct ReadSMTA1WorkflowItemQuery(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid? UserBioHubFacilityId,
    IEnumerable<string> UserPermissions);