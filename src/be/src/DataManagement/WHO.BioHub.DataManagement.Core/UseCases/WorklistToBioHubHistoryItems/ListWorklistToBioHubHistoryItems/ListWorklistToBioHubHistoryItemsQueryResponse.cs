using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ListWorklistToBioHubHistoryItems;

public record struct ListWorklistToBioHubHistoryItemsQueryResponse(IEnumerable<WorklistToBioHubItemDto> WorklistToBioHubHistoryItems) { }