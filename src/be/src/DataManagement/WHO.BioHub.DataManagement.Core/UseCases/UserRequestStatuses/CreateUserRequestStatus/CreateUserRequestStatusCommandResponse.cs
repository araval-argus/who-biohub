using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;

public record struct CreateUserRequestStatusCommandResponse(UserRequestStatus UserRequestStatus) { }