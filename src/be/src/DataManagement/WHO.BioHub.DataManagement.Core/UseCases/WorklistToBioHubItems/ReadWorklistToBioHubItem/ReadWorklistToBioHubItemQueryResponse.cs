using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;

public record struct ReadWorklistToBioHubItemQueryResponse(WorklistToBioHubItemDto WorklistToBioHubItemDto) { }