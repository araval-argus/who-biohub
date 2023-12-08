using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.ListRoles;

public record struct ListRolesQueryResponse(IEnumerable<RoleDto> Roles) { }