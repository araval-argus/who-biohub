using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.UpdateUserRequest;

public record struct UpdateUserRequestCommandResponse(UserRequest UserRequest) { }