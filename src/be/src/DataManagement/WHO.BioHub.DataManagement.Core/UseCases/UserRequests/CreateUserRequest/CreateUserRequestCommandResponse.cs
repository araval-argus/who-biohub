using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.CreateUserRequest;

public record struct CreateUserRequestCommandResponse(UserRequest UserRequest) { }