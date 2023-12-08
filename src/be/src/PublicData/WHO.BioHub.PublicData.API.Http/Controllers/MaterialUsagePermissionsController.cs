using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;
using WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IMaterialUsagePermissionsController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialUsagePermissionsController : ControllerBase, IMaterialUsagePermissionsController
{
    private readonly IReadMaterialUsagePermissionHandler _readMaterialUsagePermissionHandler;
    private readonly IListMaterialUsagePermissionsHandler _listMaterialUsagePermissionsHandler;

    public MaterialUsagePermissionsController(
        IReadMaterialUsagePermissionHandler readMaterialUsagePermissionHandler,
        IListMaterialUsagePermissionsHandler listMaterialUsagePermissionsHandler)
    {
        _readMaterialUsagePermissionHandler = readMaterialUsagePermissionHandler;
        _listMaterialUsagePermissionsHandler = listMaterialUsagePermissionsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialUsagePermissionsQueryResponse, Errors> result =
            await _listMaterialUsagePermissionsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialUsagePermissionQueryResponse, Errors> result =
            await _readMaterialUsagePermissionHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
