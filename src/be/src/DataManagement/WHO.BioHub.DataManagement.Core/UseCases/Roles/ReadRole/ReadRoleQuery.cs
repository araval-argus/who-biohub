namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.ReadRole;

public record struct ReadRoleQuery(Guid Id, IEnumerable<string> UserPermissions) { }