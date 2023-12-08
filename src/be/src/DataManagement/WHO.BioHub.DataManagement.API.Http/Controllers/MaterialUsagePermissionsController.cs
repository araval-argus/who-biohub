using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.DeleteMaterialUsagePermission;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialUsagePermissionsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class MaterialUsagePermissionsController : BaseIdentityController, IMaterialUsagePermissionsController
{
    private readonly ICreateMaterialUsagePermissionHandler _createMaterialUsagePermissionHandler;
    private readonly IReadMaterialUsagePermissionHandler _readMaterialUsagePermissionHandler;
    private readonly IUpdateMaterialUsagePermissionHandler _updateMaterialUsagePermissionHandler;
    private readonly IDeleteMaterialUsagePermissionHandler _deleteMaterialUsagePermissionHandler;
    private readonly IListMaterialUsagePermissionsHandler _listMaterialUsagePermissionsHandler;

    public MaterialUsagePermissionsController(
        ICreateMaterialUsagePermissionHandler createMaterialUsagePermissionHandler,
        IReadMaterialUsagePermissionHandler readMaterialUsagePermissionHandler,
        IUpdateMaterialUsagePermissionHandler updateMaterialUsagePermissionHandler,
        IDeleteMaterialUsagePermissionHandler deleteMaterialUsagePermissionHandler,
        IListMaterialUsagePermissionsHandler listMaterialUsagePermissionsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createMaterialUsagePermissionHandler = createMaterialUsagePermissionHandler;
        _readMaterialUsagePermissionHandler = readMaterialUsagePermissionHandler;
        _updateMaterialUsagePermissionHandler = updateMaterialUsagePermissionHandler;
        _deleteMaterialUsagePermissionHandler = deleteMaterialUsagePermissionHandler;
        _listMaterialUsagePermissionsHandler = listMaterialUsagePermissionsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateMaterialUsage);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateMaterialUsagePermissionCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialUsagePermissionCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateMaterialUsagePermissionCommandResponse, Errors> result =
            await _createMaterialUsagePermissionHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteMaterialUsage);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteMaterialUsagePermissionCommandResponse, Errors> result =
            await _deleteMaterialUsagePermissionHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterialUsage);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListMaterialUsagePermissionsQueryResponse, Errors> result =
            await _listMaterialUsagePermissionsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterialUsage);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadMaterialUsagePermissionQueryResponse, Errors> result =
            await _readMaterialUsagePermissionHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditMaterialUsage);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateMaterialUsagePermissionCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialUsagePermissionCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateMaterialUsagePermissionCommandResponse, Errors> result =
            await _updateMaterialUsagePermissionHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
