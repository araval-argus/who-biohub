using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;

public record struct ListMaterialUsagePermissionsQueryResponse(IEnumerable<MaterialUsagePermissionPublicDto> MaterialUsagePermissions) { }