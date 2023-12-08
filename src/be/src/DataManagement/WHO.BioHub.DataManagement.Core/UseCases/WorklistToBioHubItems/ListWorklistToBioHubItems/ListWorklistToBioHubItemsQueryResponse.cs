using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItems;

public record struct ListWorklistToBioHubItemsQueryResponse(IEnumerable<WorklistToBioHubItemDto> WorklistToBioHubItems) { }