using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;

public record struct ReadWorklistToBioHubHistoryItemQueryResponse(WorklistToBioHubItemDto WorklistToBioHubHistoryItem) { }