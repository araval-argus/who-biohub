using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ReadUserRequest;

public record struct ReadUserRequestQueryResponse(UserRequestViewModel UserRequest) { }