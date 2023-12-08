using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;

public record struct ReadWorklistFromBioHubItemQueryResponse(WorklistFromBioHubItemDto WorklistFromBioHubItemDto) { }