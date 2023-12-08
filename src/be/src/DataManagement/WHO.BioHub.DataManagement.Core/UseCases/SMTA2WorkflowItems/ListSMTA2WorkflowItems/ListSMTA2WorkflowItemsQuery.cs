using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;

public record struct ListSMTA2WorkflowItemsQuery(
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    IEnumerable<string> UserPermissions);