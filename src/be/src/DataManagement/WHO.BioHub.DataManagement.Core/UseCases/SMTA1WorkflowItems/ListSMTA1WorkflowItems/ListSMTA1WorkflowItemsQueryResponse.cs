using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;

public record struct ListSMTA1WorkflowItemsQueryResponse(IEnumerable<SMTA1WorkflowItemDto> SMTA1WorkflowItems) { }