using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;

public record struct ListSMTA2WorkflowItemsQueryResponse(IEnumerable<SMTA2WorkflowItemDto> SMTA2WorkflowItems) { }