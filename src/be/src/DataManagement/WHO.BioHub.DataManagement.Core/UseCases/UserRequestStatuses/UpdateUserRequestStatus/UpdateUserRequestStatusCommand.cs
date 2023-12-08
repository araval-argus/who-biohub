using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;

public record struct UpdateUserRequestStatusCommand(Guid Id,
    string Message,
    bool IsResponseMessage,
    UserRegistrationStatus Status);