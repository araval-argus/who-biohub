using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.CreateTransportMode;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.DeleteTransportMode;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ListTransportModes;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ReadTransportMode;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.UpdateTransportMode;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ITransportModesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class TransportModesController : BaseIdentityController, ITransportModesController
{
    private readonly ICreateTransportModeHandler _createTransportModeHandler;
    private readonly IReadTransportModeHandler _readTransportModeHandler;
    private readonly IUpdateTransportModeHandler _updateTransportModeHandler;
    private readonly IDeleteTransportModeHandler _deleteTransportModeHandler;
    private readonly IListTransportModesHandler _listTransportModesHandler;

    public TransportModesController(
        ICreateTransportModeHandler createTransportModeHandler,
        IReadTransportModeHandler readTransportModeHandler,
        IUpdateTransportModeHandler updateTransportModeHandler,
        IDeleteTransportModeHandler deleteTransportModeHandler,
        IListTransportModesHandler listTransportModesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createTransportModeHandler = createTransportModeHandler;
        _readTransportModeHandler = readTransportModeHandler;
        _updateTransportModeHandler = updateTransportModeHandler;
        _deleteTransportModeHandler = deleteTransportModeHandler;
        _listTransportModesHandler = listTransportModesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateTransportMode);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateTransportModeCommand, Errors> body =
            await request.ParseBodyJson<CreateTransportModeCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateTransportModeCommandResponse, Errors> result =
            await _createTransportModeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteTransportMode);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteTransportModeCommandResponse, Errors> result =
            await _deleteTransportModeHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadTransportMode);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListTransportModesQueryResponse, Errors> result =
            await _listTransportModesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadTransportMode);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadTransportModeQueryResponse, Errors> result =
            await _readTransportModeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditTransportMode);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateTransportModeCommand, Errors> body =
            await request.ParseBodyJson<UpdateTransportModeCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateTransportModeCommandResponse, Errors> result =
            await _updateTransportModeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
