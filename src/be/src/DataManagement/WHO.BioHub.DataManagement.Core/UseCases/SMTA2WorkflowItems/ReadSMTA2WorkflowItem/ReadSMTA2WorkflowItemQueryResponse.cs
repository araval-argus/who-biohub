using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;

public record struct ReadSMTA2WorkflowItemQueryResponse(SMTA2WorkflowItemDto SMTA2WorkflowItemDto) { }