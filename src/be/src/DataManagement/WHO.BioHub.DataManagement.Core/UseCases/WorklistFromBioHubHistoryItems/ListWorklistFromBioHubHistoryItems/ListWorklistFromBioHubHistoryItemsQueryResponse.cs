using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ListWorklistFromBioHubHistoryItems;

public record struct ListWorklistFromBioHubHistoryItemsQueryResponse(IEnumerable<WorklistFromBioHubItemDto> WorklistFromBioHubHistoryItems) { }