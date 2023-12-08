using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;

public record struct ListUsersQuery(bool OnlyCouriers, RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    IEnumerable<string> UserPermissions)
{ }