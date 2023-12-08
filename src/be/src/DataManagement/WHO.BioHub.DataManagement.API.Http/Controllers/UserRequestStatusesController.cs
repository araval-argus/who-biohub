using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.DeleteUserRequestStatus;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ListUserRequestStatuses;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Enums;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IUserRequestStatusesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);

    Task<IActionResult> ReadByStatus(HttpRequest request, ILogger log, UserRegistrationStatus status, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class UserRequestStatusesController : BaseIdentityController, IUserRequestStatusesController
{
    private readonly ICreateUserRequestStatusHandler _createUserRequestStatusHandler;
    private readonly IReadUserRequestStatusHandler _readUserRequestStatusHandler;
    private readonly IReadUserRequestStatusByStatusHandler _readUserRequestStatusByStatusHandler;
    private readonly IUpdateUserRequestStatusHandler _updateUserRequestStatusHandler;
    private readonly IDeleteUserRequestStatusHandler _deleteUserRequestStatusHandler;
    private readonly IListUserRequestStatusesHandler _listUserRequestStatusesHandler;

    public UserRequestStatusesController(
        ICreateUserRequestStatusHandler createUserRequestStatusHandler,
        IReadUserRequestStatusHandler readUserRequestStatusHandler,
        IReadUserRequestStatusByStatusHandler readUserRequestStatusByStatusHandler,
        IUpdateUserRequestStatusHandler updateUserRequestStatusHandler,
        IDeleteUserRequestStatusHandler deleteUserRequestStatusHandler,
        IListUserRequestStatusesHandler listUserRequestStatusesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createUserRequestStatusHandler = createUserRequestStatusHandler;
        _readUserRequestStatusHandler = readUserRequestStatusHandler;
        _readUserRequestStatusByStatusHandler = readUserRequestStatusByStatusHandler;
        _updateUserRequestStatusHandler = updateUserRequestStatusHandler;
        _deleteUserRequestStatusHandler = deleteUserRequestStatusHandler;
        _listUserRequestStatusesHandler = listUserRequestStatusesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateUserRequestStatus);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateUserRequestStatusCommand, Errors> body =
            await request.ParseBodyJson<CreateUserRequestStatusCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateUserRequestStatusCommandResponse, Errors> result =
            await _createUserRequestStatusHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteUserRequestStatus);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteUserRequestStatusCommandResponse, Errors> result =
            await _deleteUserRequestStatusHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUserRequestStatus);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListUserRequestStatusesQueryResponse, Errors> result =
            await _listUserRequestStatusesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUserRequestStatus);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadUserRequestStatusQueryResponse, Errors> result =
            await _readUserRequestStatusHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadByStatus(HttpRequest request, ILogger log, UserRegistrationStatus status, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUserRequestStatus);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadUserRequestStatusByStatusQueryResponse, Errors> result =
            await _readUserRequestStatusByStatusHandler.Handle(new(Status: status), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditUserRequestStatus);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateUserRequestStatusCommand, Errors> body =
            await request.ParseBodyJson<UpdateUserRequestStatusCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateUserRequestStatusCommandResponse, Errors> result =
            await _updateUserRequestStatusHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
