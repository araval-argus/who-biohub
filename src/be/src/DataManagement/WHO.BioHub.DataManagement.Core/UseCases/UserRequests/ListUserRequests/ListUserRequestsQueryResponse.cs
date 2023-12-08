using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ListUserRequests;

public record struct ListUserRequestsQueryResponse(IEnumerable<UserRequestViewModel> UserRequests) { }