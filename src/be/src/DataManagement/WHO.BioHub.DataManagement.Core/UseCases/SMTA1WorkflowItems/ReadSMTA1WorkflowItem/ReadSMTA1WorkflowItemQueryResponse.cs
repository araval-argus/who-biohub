using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;

public record struct ReadSMTA1WorkflowItemQueryResponse(SMTA1WorkflowItemDto SMTA1WorkflowItemDto) { }