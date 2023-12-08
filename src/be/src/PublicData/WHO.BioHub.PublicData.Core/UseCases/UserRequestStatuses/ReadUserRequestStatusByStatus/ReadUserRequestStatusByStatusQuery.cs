using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public record struct ReadUserRequestStatusByStatusQuery(UserRegistrationStatus Status) { }