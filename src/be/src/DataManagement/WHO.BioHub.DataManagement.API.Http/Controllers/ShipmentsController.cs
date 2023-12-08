using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.CreateShipment;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.DeleteShipment;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;
using WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IShipmentsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class ShipmentsController : BaseIdentityController, IShipmentsController
{
    private readonly ICreateShipmentHandler _createShipmentHandler;
    private readonly IReadShipmentHandler _readShipmentHandler;
    private readonly IUpdateShipmentHandler _updateShipmentHandler;
    private readonly IDeleteShipmentHandler _deleteShipmentHandler;
    private readonly IListShipmentsHandler _listShipmentsHandler;

    public ShipmentsController(
        ICreateShipmentHandler createShipmentHandler,
        IReadShipmentHandler readShipmentHandler,
        IUpdateShipmentHandler updateShipmentHandler,
        IDeleteShipmentHandler deleteShipmentHandler,
        IListShipmentsHandler listShipmentsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createShipmentHandler = createShipmentHandler;
        _readShipmentHandler = readShipmentHandler;
        _updateShipmentHandler = updateShipmentHandler;
        _deleteShipmentHandler = deleteShipmentHandler;
        _listShipmentsHandler = listShipmentsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateShipment);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateShipmentCommand, Errors> body =
            await request.ParseBodyJson<CreateShipmentCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateShipmentCommandResponse, Errors> result =
            await _createShipmentHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteShipment);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteShipmentCommandResponse, Errors> result =
            await _deleteShipmentHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadShipment);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListShipmentsQueryResponse, Errors> result =
            await _listShipmentsHandler.Handle(new(RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadShipment);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ReadShipmentQueryResponse, Errors> result =
            await _readShipmentHandler.Handle(new(Id: id, RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId, userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditShipment);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateShipmentCommand, Errors> body =
            await request.ParseBodyJson<UpdateShipmentCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateShipmentCommandResponse, Errors> result =
            await _updateShipmentHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
