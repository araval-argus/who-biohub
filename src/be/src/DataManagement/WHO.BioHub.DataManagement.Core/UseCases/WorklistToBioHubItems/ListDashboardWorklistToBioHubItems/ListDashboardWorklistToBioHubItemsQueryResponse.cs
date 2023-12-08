using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItems;

public record struct ListDashboardWorklistToBioHubItemsQueryResponse(IEnumerable<WorklistToBioHubItemDto> WorklistToBioHubItems) { }