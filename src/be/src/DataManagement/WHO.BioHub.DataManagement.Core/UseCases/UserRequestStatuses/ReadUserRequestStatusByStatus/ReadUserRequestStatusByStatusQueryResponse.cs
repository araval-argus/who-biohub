using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public record struct ReadUserRequestStatusByStatusQueryResponse(UserRequestStatus UserRequestStatus) { }