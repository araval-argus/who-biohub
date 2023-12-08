namespace WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUserFromUserRequest;

public record struct CreateUserFromUserRequestCommand(
    string FirstName,
    string LastName,
    string Email,
    Guid? RoleId,
    Guid? CountryId,
    Guid? LaboratoryId,
    Guid? OperationById
    );