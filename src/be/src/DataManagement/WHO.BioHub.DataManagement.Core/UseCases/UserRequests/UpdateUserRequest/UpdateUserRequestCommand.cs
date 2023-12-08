using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;

public record struct UpdateUserRequestCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Purpose,
    bool TermsAndConditionAccepted,
    UserRegistrationStatus Status,
    Guid? RoleId,
    Guid? CountryId,
    Guid? LaboratoryId,
    string Message,
    bool IsConfirmed,
    DateTime? ConfirmationDate);