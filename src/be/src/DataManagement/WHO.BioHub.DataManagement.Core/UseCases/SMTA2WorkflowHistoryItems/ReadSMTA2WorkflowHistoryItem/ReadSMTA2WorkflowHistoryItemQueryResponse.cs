using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;

public record struct ReadSMTA2WorkflowHistoryItemQueryResponse(SMTA2WorkflowItemDto SMTA2WorkflowHistoryItem) { }