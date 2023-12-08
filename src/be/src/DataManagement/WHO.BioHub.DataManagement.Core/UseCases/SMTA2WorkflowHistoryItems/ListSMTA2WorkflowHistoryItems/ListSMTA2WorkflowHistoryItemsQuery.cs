using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ListSMTA2WorkflowHistoryItems;

public record struct ListSMTA2WorkflowHistoryItemsQuery(
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    Guid WorlistToBioHubItemId,
    IEnumerable<string> UserPermissions
    );