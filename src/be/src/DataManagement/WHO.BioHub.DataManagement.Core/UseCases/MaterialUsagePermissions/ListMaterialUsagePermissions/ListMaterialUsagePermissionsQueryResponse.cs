using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;

public record struct ListMaterialUsagePermissionsQueryResponse(IEnumerable<MaterialUsagePermissionDto> MaterialUsagePermissions) { }