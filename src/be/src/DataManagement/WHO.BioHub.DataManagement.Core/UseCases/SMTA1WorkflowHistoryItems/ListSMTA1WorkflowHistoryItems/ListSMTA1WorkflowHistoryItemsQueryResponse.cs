using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ListSMTA1WorkflowHistoryItems;

public record struct ListSMTA1WorkflowHistoryItemsQueryResponse(IEnumerable<SMTA1WorkflowItemDto> SMTA1WorkflowHistoryItems) { }