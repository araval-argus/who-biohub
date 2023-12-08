using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;

public record struct UpdateUserRequestCommandResponse(UserRequest UserRequest) { }