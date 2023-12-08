using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public record struct ReadUserRequestStatusQueryResponse(UserRequestStatus UserRequestStatus) { }