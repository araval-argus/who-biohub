using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;

public record struct CreateSMTA1WorkflowItemCommand(
    Guid? LaboratoryId,
    Guid? UserId,
    bool? IsPast,
    DateTime? AssignedOperationDate);