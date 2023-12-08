using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.DeleteBSLLevel;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ListBSLLevels;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ReadBSLLevel;
using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IBSLLevelsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class BSLLevelsController : BaseIdentityController, IBSLLevelsController
{
    private readonly ICreateBSLLevelHandler _createBSLLevelHandler;
    private readonly IReadBSLLevelHandler _readBSLLevelHandler;
    private readonly IUpdateBSLLevelHandler _updateBSLLevelHandler;
    private readonly IDeleteBSLLevelHandler _deleteBSLLevelHandler;
    private readonly IListBSLLevelsHandler _listBSLLevelsHandler;

    public BSLLevelsController(
        ICreateBSLLevelHandler createBSLLevelHandler,
        IReadBSLLevelHandler readBSLLevelHandler,
        IUpdateBSLLevelHandler updateBSLLevelHandler,
        IDeleteBSLLevelHandler deleteBSLLevelHandler,
        IListBSLLevelsHandler listBSLLevelsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createBSLLevelHandler = createBSLLevelHandler;
        _readBSLLevelHandler = readBSLLevelHandler;
        _updateBSLLevelHandler = updateBSLLevelHandler;
        _deleteBSLLevelHandler = deleteBSLLevelHandler;
        _listBSLLevelsHandler = listBSLLevelsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateBSLLevel);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateBSLLevelCommand, Errors> body =
            await request.ParseBodyJson<CreateBSLLevelCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateBSLLevelCommandResponse, Errors> result =
            await _createBSLLevelHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteBSLLevel);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteBSLLevelCommandResponse, Errors> result =
            await _deleteBSLLevelHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadBSLLevel);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListBSLLevelsQueryResponse, Errors> result =
            await _listBSLLevelsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadBSLLevel);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadBSLLevelQueryResponse, Errors> result =
            await _readBSLLevelHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditBSLLevel);

        Either<UpdateBSLLevelCommand, Errors> body =
            await request.ParseBodyJson<UpdateBSLLevelCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateBSLLevelCommandResponse, Errors> result =
            await _updateBSLLevelHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
