using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.CreateUserRequest;

public record struct CreateUserRequestCommand(
    string FirstName,
    string LastName,
    string Email,
    string Purpose,
    bool TermsAndConditionAccepted,
    UserRegistrationStatus Status,
    Guid? RoleId,
    Guid? CountryId,
    string InstituteName,
    string RecaptchaResponse);