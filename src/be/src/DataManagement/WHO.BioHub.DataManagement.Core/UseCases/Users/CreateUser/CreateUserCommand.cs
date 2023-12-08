namespace WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUser;

public record struct CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string MobilePhone,
    string BusinessPhone,
    string JobTitle,
    bool? OperationalFocalPoint,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    Guid? CourierId,
    Guid? RoleId,
    string Notes,
    Guid? OperationById
    );