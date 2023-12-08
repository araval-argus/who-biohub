using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;

public record struct ReadSMTA2WorkflowHistoryItemQuery(Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    IEnumerable<string> UserPermissions);