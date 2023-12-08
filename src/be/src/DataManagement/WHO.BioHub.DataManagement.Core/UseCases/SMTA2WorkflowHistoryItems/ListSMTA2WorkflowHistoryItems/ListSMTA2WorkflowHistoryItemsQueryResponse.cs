using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ListSMTA2WorkflowHistoryItems;

public record struct ListSMTA2WorkflowHistoryItemsQueryResponse(IEnumerable<SMTA2WorkflowItemDto> SMTA2WorkflowHistoryItems) { }