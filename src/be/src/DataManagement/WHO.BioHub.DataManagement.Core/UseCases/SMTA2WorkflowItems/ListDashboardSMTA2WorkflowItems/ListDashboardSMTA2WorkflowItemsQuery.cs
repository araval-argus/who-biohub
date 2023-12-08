using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItems;

public record struct ListDashboardSMTA2WorkflowItemsQuery(
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    IEnumerable<string> UserPermissions);