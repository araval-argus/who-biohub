using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;

public record struct CreateUserRequestStatusCommand(
    string Message,
    bool IsResponseMessage,
    UserRegistrationStatus Status);