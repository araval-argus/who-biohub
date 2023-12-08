using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItems;

public record struct ListDashboardSMTA2WorkflowItemsQueryResponse(IEnumerable<SMTA2WorkflowItemDto> SMTA2WorkflowItems) { }