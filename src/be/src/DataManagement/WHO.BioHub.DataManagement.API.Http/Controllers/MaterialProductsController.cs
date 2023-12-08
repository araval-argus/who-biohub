using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.DeleteMaterialProduct;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ListMaterialProducts;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ReadMaterialProduct;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.UpdateMaterialProduct;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialProductsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class MaterialProductsController : BaseIdentityController, IMaterialProductsController
{
    private readonly ICreateMaterialProductHandler _createMaterialProductHandler;
    private readonly IReadMaterialProductHandler _readMaterialProductHandler;
    private readonly IUpdateMaterialProductHandler _updateMaterialProductHandler;
    private readonly IDeleteMaterialProductHandler _deleteMaterialProductHandler;
    private readonly IListMaterialProductsHandler _listMaterialProductsHandler;

    public MaterialProductsController(
        ICreateMaterialProductHandler createMaterialProductHandler,
        IReadMaterialProductHandler readMaterialProductHandler,
        IUpdateMaterialProductHandler updateMaterialProductHandler,
        IDeleteMaterialProductHandler deleteMaterialProductHandler,
        IListMaterialProductsHandler listMaterialProductsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createMaterialProductHandler = createMaterialProductHandler;
        _readMaterialProductHandler = readMaterialProductHandler;
        _updateMaterialProductHandler = updateMaterialProductHandler;
        _deleteMaterialProductHandler = deleteMaterialProductHandler;
        _listMaterialProductsHandler = listMaterialProductsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateMaterialProduct);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateMaterialProductCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialProductCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateMaterialProductCommandResponse, Errors> result =
            await _createMaterialProductHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteMaterialProduct);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteMaterialProductCommandResponse, Errors> result =
            await _deleteMaterialProductHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterialProduct);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListMaterialProductsQueryResponse, Errors> result =
            await _listMaterialProductsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterialProduct);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadMaterialProductQueryResponse, Errors> result =
            await _readMaterialProductHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditMaterialProduct);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateMaterialProductCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialProductCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateMaterialProductCommandResponse, Errors> result =
            await _updateMaterialProductHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
