using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.CreateCourier;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.DeleteCourier;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.ListCouriers;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.ReadCourier;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.UpdateCourier;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ICouriersController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class CouriersController : BaseIdentityController, ICouriersController
{
    private readonly ICreateCourierHandler _createCourierHandler;
    private readonly IReadCourierHandler _readCourierHandler;
    private readonly IUpdateCourierHandler _updateCourierHandler;
    private readonly IDeleteCourierHandler _deleteCourierHandler;
    private readonly IListCouriersHandler _listCouriersHandler;

    public CouriersController(
        ICreateCourierHandler createCourierHandler,
        IReadCourierHandler readCourierHandler,
        IUpdateCourierHandler updateCourierHandler,
        IDeleteCourierHandler deleteCourierHandler,
        IListCouriersHandler listCouriersHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createCourierHandler = createCourierHandler;
        _readCourierHandler = readCourierHandler;
        _updateCourierHandler = updateCourierHandler;
        _deleteCourierHandler = deleteCourierHandler;
        _listCouriersHandler = listCouriersHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateCourierCommand, Errors> body =
            await request.ParseBodyJson<CreateCourierCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<CreateCourierCommandResponse, Errors> result =
            await _createCourierHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

       
        Either<DeleteCourierCommandResponse, Errors> result =
            await _deleteCourierHandler.Handle(new(Id: id, OperationById: userLoginInfo.UserId), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListCouriersQueryResponse, Errors> result =
            await _listCouriersHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadCourierQueryResponse, Errors> result =
            await _readCourierHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
           await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateCourierCommand, Errors> body =
            await request.ParseBodyJson<UpdateCourierCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<UpdateCourierCommandResponse, Errors> result =
            await _updateCourierHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
