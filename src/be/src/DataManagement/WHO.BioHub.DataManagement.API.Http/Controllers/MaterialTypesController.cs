using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.DeleteMaterialType;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ListMaterialTypes;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ReadMaterialType;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialTypesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class MaterialTypesController : BaseIdentityController, IMaterialTypesController
{
    private readonly ICreateMaterialTypeHandler _createMaterialTypeHandler;
    private readonly IReadMaterialTypeHandler _readMaterialTypeHandler;
    private readonly IUpdateMaterialTypeHandler _updateMaterialTypeHandler;
    private readonly IDeleteMaterialTypeHandler _deleteMaterialTypeHandler;
    private readonly IListMaterialTypesHandler _listMaterialTypesHandler;

    public MaterialTypesController(
        ICreateMaterialTypeHandler createMaterialTypeHandler,
        IReadMaterialTypeHandler readMaterialTypeHandler,
        IUpdateMaterialTypeHandler updateMaterialTypeHandler,
        IDeleteMaterialTypeHandler deleteMaterialTypeHandler,
        IListMaterialTypesHandler listMaterialTypesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createMaterialTypeHandler = createMaterialTypeHandler;
        _readMaterialTypeHandler = readMaterialTypeHandler;
        _updateMaterialTypeHandler = updateMaterialTypeHandler;
        _deleteMaterialTypeHandler = deleteMaterialTypeHandler;
        _listMaterialTypesHandler = listMaterialTypesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateMaterialType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateMaterialTypeCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialTypeCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateMaterialTypeCommandResponse, Errors> result =
            await _createMaterialTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteMaterialType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteMaterialTypeCommandResponse, Errors> result =
            await _deleteMaterialTypeHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterialType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListMaterialTypesQueryResponse, Errors> result =
            await _listMaterialTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterialType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadMaterialTypeQueryResponse, Errors> result =
            await _readMaterialTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditMaterialType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateMaterialTypeCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialTypeCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateMaterialTypeCommandResponse, Errors> result =
            await _updateMaterialTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
