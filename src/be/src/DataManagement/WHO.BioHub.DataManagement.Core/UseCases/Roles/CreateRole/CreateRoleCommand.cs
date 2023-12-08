using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.CreateRole;

public record struct CreateRoleCommand(
    string Name,
    string Description,
    RoleType RoleType,
    bool AddToRegistration);