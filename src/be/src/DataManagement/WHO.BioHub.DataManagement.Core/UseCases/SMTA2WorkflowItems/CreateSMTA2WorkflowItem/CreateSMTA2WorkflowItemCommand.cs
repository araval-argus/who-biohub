namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.CreateSMTA2WorkflowItem;

public record struct CreateSMTA2WorkflowItemCommand(
    Guid? LaboratoryId,
    Guid? UserId,
    bool? IsPast,
    DateTime? AssignedOperationDate);