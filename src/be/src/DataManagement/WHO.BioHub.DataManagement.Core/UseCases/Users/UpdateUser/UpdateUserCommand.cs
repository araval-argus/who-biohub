namespace WHO.BioHub.DataManagement.Core.UseCases.Users.UpdateUser;

public record struct UpdateUserCommand(Guid Id,
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
    string Notes,
    Guid? RoleId,
    Guid? OperationById);