using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.CreateUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.DeleteUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ListUserRequests;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ReadUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ApproveOrRejectUserRequest;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IUserRequestsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ApproveOrReject(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);

}


public class UserRequestsController : BaseIdentityController, IUserRequestsController
{
    private readonly ICreateUserRequestHandler _createUserRequestHandler;
    private readonly IReadUserRequestHandler _readUserRequestHandler;
    private readonly IUpdateUserRequestHandler _updateUserRequestHandler;
    private readonly IDeleteUserRequestHandler _deleteUserRequestHandler;
    private readonly IListUserRequestsHandler _listUserRequestsHandler;
    private readonly IApproveOrRejectUserRequestHandler _approveOrRejectUserRequestHandler;

    public UserRequestsController(
        ICreateUserRequestHandler createUserRequestHandler,
        IReadUserRequestHandler readUserRequestHandler,
        IUpdateUserRequestHandler updateUserRequestHandler,
        IDeleteUserRequestHandler deleteUserRequestHandler,
        IListUserRequestsHandler listUserRequestsHandler,
        IApproveOrRejectUserRequestHandler approveOrRejectUserRequestHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createUserRequestHandler = createUserRequestHandler;
        _readUserRequestHandler = readUserRequestHandler;
        _updateUserRequestHandler = updateUserRequestHandler;
        _deleteUserRequestHandler = deleteUserRequestHandler;
        _listUserRequestsHandler = listUserRequestsHandler;
        _approveOrRejectUserRequestHandler = approveOrRejectUserRequestHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateUserRequest);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateUserRequestCommand, Errors> body =
            await request.ParseBodyJson<CreateUserRequestCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateUserRequestCommandResponse, Errors> result =
            await _createUserRequestHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteUserRequest);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteUserRequestCommandResponse, Errors> result =
            await _deleteUserRequestHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUserRequest);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListUserRequestsQueryResponse, Errors> result =
            await _listUserRequestsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUserRequest);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadUserRequestQueryResponse, Errors> result =
            await _readUserRequestHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditUserRequest);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateUserRequestCommand, Errors> body =
            await request.ParseBodyJson<UpdateUserRequestCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateUserRequestCommandResponse, Errors> result =
            await _updateUserRequestHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ApproveOrReject(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanApproveOrRejectUserRequest);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ApproveOrRejectUserRequestCommand, Errors> body =
            await request.ParseBodyJson<ApproveOrRejectUserRequestCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<ApproveOrRejectUserRequestCommandResponse, Errors> result =
            await _approveOrRejectUserRequestHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
