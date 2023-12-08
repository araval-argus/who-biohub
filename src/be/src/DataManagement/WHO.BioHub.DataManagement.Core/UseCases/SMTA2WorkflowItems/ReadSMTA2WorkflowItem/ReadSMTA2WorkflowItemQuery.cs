using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;

public record struct ReadSMTA2WorkflowItemQuery(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    IEnumerable<string> UserPermissions);