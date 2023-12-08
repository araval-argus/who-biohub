using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;

public record struct ListWorklistFromBioHubItemsQueryResponse(IEnumerable<WorklistFromBioHubItemDto> WorklistFromBioHubItems) { }