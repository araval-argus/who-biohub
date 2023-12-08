using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;

public record struct UpdateUserRequestStatusCommandResponse(UserRequestStatus UserRequestStatus) { }