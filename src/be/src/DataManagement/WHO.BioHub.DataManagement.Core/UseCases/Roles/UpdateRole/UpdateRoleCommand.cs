using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.UpdateRole;

public record struct UpdateRoleCommand(Guid Id,
    string Name,
    string Description,
    RoleType RoleType,
    bool AddToRegistration);