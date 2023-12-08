using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Roles.ListRoles;

public record struct ListRolesQueryResponse(IEnumerable<RolePublicViewModel> Roles) { }