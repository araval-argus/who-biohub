namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.ListRoles;

public record struct ListRolesQuery(IEnumerable<string> UserPermissions) { }