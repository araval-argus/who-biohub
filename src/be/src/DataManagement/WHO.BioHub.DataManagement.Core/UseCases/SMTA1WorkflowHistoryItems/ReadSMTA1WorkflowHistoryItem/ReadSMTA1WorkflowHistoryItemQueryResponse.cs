using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;

public record struct ReadSMTA1WorkflowHistoryItemQueryResponse(SMTA1WorkflowItemDto SMTA1WorkflowHistoryItem) { }