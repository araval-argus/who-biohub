using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public record struct ReadUserRequestStatusByStatusQuery(UserRegistrationStatus Status) { }