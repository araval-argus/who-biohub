using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListCourierUsers;

public record struct ListCourierUsersForWorklistToBioHubItemQueryResponse(IEnumerable<WorklistItemUserDto> WorklistToBioHubItemUsers) { }