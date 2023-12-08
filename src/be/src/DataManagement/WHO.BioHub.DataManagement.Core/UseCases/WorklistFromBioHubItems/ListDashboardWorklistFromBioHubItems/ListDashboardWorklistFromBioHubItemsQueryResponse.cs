using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItems;

public record struct ListDashboardWorklistFromBioHubItemsQueryResponse(IEnumerable<WorklistFromBioHubItemDto> WorklistFromBioHubItems) { }