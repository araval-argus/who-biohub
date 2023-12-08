using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;

public record struct ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryResponse(IEnumerable<WorklistItemUserDto> WorklistFromBioHubItemUsers) { }