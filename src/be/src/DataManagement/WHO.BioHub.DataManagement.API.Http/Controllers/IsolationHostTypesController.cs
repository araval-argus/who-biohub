using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.DeleteIsolationHostType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ReadIsolationHostType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.UpdateIsolationHostType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IIsolationHostTypesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class IsolationHostTypesController : BaseIdentityController, IIsolationHostTypesController
{
    private readonly ICreateIsolationHostTypeHandler _createIsolationHostTypeHandler;
    private readonly IReadIsolationHostTypeHandler _readIsolationHostTypeHandler;
    private readonly IUpdateIsolationHostTypeHandler _updateIsolationHostTypeHandler;
    private readonly IDeleteIsolationHostTypeHandler _deleteIsolationHostTypeHandler;
    private readonly IListIsolationHostTypesHandler _listIsolationHostTypesHandler;

    public IsolationHostTypesController(
        ICreateIsolationHostTypeHandler createIsolationHostTypeHandler,
        IReadIsolationHostTypeHandler readIsolationHostTypeHandler,
        IUpdateIsolationHostTypeHandler updateIsolationHostTypeHandler,
        IDeleteIsolationHostTypeHandler deleteIsolationHostTypeHandler,
        IListIsolationHostTypesHandler listIsolationHostTypesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createIsolationHostTypeHandler = createIsolationHostTypeHandler;
        _readIsolationHostTypeHandler = readIsolationHostTypeHandler;
        _updateIsolationHostTypeHandler = updateIsolationHostTypeHandler;
        _deleteIsolationHostTypeHandler = deleteIsolationHostTypeHandler;
        _listIsolationHostTypesHandler = listIsolationHostTypesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateIsolationHostType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateIsolationHostTypeCommand, Errors> body =
            await request.ParseBodyJson<CreateIsolationHostTypeCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateIsolationHostTypeCommandResponse, Errors> result =
            await _createIsolationHostTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
           await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteIsolationHostType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteIsolationHostTypeCommandResponse, Errors> result =
            await _deleteIsolationHostTypeHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
           await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadIsolationHostType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListIsolationHostTypesQueryResponse, Errors> result =
            await _listIsolationHostTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
           await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadIsolationHostType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadIsolationHostTypeQueryResponse, Errors> result =
            await _readIsolationHostTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
           await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditIsolationHostType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateIsolationHostTypeCommand, Errors> body =
            await request.ParseBodyJson<UpdateIsolationHostTypeCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateIsolationHostTypeCommandResponse, Errors> result =
            await _updateIsolationHostTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
