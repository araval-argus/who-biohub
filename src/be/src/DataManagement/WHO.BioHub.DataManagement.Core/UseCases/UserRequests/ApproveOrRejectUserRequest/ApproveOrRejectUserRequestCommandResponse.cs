using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ApproveOrRejectUserRequest;

public record struct ApproveOrRejectUserRequestCommandResponse(UserRequest UserRequest) { }