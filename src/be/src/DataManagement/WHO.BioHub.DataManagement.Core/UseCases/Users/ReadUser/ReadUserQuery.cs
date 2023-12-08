using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;

public record struct ReadUserQuery(Guid Id, bool OnlyCouriers, RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    IEnumerable<string> UserPermissions)
{ }