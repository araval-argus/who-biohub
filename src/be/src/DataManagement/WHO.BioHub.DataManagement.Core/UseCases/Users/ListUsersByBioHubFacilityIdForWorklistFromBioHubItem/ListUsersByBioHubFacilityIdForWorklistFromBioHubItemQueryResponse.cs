using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;

public record struct ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryResponse(IEnumerable<WorklistItemUserDto> WorklistFromBioHubItemUsers) { }