using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.DeleteUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.UpdateUser;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUserByExternalId;
using WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUserFromUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Graph;
using System.Security.Claims;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListCourierUsers;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IUsersController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ListUsersByLaboratoryIdForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid laboratoryId, Guid worklistToBioHubItemId, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> CreateFromUserRequest(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ListUsersByBioHubFacilityIdForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid bioHubFacilityId, Guid worklistToBioHubItemId, CancellationToken cancellationToken);
    Task<IActionResult> ListCourierUsersForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken);

    Task<IActionResult> ListCourierUsers(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> CreateCourierUser(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ReadCourierUser(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> UpdateCourierUser(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> DeleteCourierUser(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ListUsersByLaboratoryIdForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid laboratoryId, Guid worklistFromBioHubItemId, CancellationToken cancellationToken);
    Task<IActionResult> ListUsersByBioHubFacilityIdForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid bioHubFacilityId, Guid worklistFromBioHubItemId, CancellationToken cancellationToken);
    Task<IActionResult> ListCourierUsersForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid worklistFromBioHubItemId, CancellationToken cancellationToken);

}

public class UsersController : BaseIdentityController, IUsersController
{
    private readonly ICreateUserHandler _createUserHandler;
    private readonly IReadUserHandler _readUserHandler;
    private readonly IListUsersByLaboratoryIdForWorklistToBioHubItemHandler _readUserByLaboratoryIdForWorklistToBioHubItemHandler;
    private readonly IListUsersByBioHubFacilityIdForWorklistToBioHubItemHandler _readUserByBioHubFacilityIdForWorklistToBioHubItemHandler;
    private readonly IListCourierUsersForWorklistToBioHubItemHandler _readCourierUserForWorklistToBioHubItemHandler;
    private readonly IListUsersByLaboratoryIdForWorklistFromBioHubItemHandler _readUserByLaboratoryIdForWorklistFromBioHubItemHandler;
    private readonly IListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler _readUserByBioHubFacilityIdForWorklistFromBioHubItemHandler;
    private readonly IListCourierUsersForWorklistFromBioHubItemHandler _readCourierUserForWorklistFromBioHubItemHandler;
    private readonly IReadUserByExternalIdHandler _readUserByExternalIdHandler;
    private readonly IUpdateUserHandler _updateUserHandler;
    private readonly IDeleteUserHandler _deleteUserHandler;
    private readonly IListUsersHandler _listUsersHandler;
    private readonly ICreateUserFromUserRequestHandler _createUserFromUserRequestHandler;
    private readonly IAzureADUserInvitation _azureADUserInvitation;

    public UsersController(
        ICreateUserHandler createUserHandler,
        IReadUserHandler readUserHandler,
        IListUsersByLaboratoryIdForWorklistToBioHubItemHandler readUserByLaboratoryIdForWorklistToBioHubItemHandler,
        IListUsersByBioHubFacilityIdForWorklistToBioHubItemHandler readUserByBioHubFacilityIdForWorklistToBioHubItemHandler,
        IListCourierUsersForWorklistToBioHubItemHandler readCourierUserForWorklistToBioHubItemHandler,
        IListUsersByLaboratoryIdForWorklistFromBioHubItemHandler readUserByLaboratoryIdForWorklistFromBioHubItemHandler,
        IListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler readUserByBioHubFacilityIdForWorklistFromBioHubItemHandler,
        IListCourierUsersForWorklistFromBioHubItemHandler readCourierUserForWorklistFromBioHubItemHandler,
        IReadUserByExternalIdHandler readUserByExternalIdHandler,
        IUpdateUserHandler updateUserHandler,
        IDeleteUserHandler deleteUserHandler,
        IListUsersHandler listUsersHandler,
        ICreateUserFromUserRequestHandler createUserFromUserRequestHandler,
        IAzureADUserInvitation azureADUserInvitation,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createUserHandler = createUserHandler;
        _readUserHandler = readUserHandler;
        _readUserByLaboratoryIdForWorklistToBioHubItemHandler = readUserByLaboratoryIdForWorklistToBioHubItemHandler;
        _readUserByExternalIdHandler = readUserByExternalIdHandler;
        _updateUserHandler = updateUserHandler;
        _deleteUserHandler = deleteUserHandler;
        _listUsersHandler = listUsersHandler;
        _createUserFromUserRequestHandler = createUserFromUserRequestHandler;
        _azureADUserInvitation = azureADUserInvitation;
        _readUserByBioHubFacilityIdForWorklistToBioHubItemHandler = readUserByBioHubFacilityIdForWorklistToBioHubItemHandler;
        _readCourierUserForWorklistToBioHubItemHandler = readCourierUserForWorklistToBioHubItemHandler;
        _readUserByLaboratoryIdForWorklistFromBioHubItemHandler = readUserByLaboratoryIdForWorklistFromBioHubItemHandler;
        _readUserByBioHubFacilityIdForWorklistFromBioHubItemHandler = readUserByBioHubFacilityIdForWorklistFromBioHubItemHandler;
        _readCourierUserForWorklistFromBioHubItemHandler = readCourierUserForWorklistFromBioHubItemHandler;

    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateUserCommand, Errors> body =
            await request.ParseBodyJson<CreateUserCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<CreateUserCommandResponse, Errors> result =
            await _createUserHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        string accessToken = GetAccessToken(request, log);

        if (!string.IsNullOrEmpty(accessToken))
        {
            var sendInvitationStatus = await _azureADUserInvitation.InviteUserAsync(body.Left.Email, accessToken, body.Left.FirstName, body.Left.LastName);
        }

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> CreateCourierUser(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateCourierStaff);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateUserCommand, Errors> body =
            await request.ParseBodyJson<CreateUserCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<CreateUserCommandResponse, Errors> result =
            await _createUserHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
             await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<DeleteUserCommandResponse, Errors> result =
            await _deleteUserHandler.Handle(new(Id: id, OperationById: checkUserPermissionResult.Left.UserId), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> DeleteCourierUser(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
             await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteCourierStaff);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<DeleteUserCommandResponse, Errors> result =
            await _deleteUserHandler.Handle(new(Id: id, OperationById: checkUserPermissionResult.Left.UserId), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ListUsersQueryResponse, Errors> result =
            await _listUsersHandler.Handle(new(OnlyCouriers: false, RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListCourierUsers(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourierStaff);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ListUsersQueryResponse, Errors> result =
            await _listUsersHandler.Handle(new(OnlyCouriers: true, RoleType: null, LaboratoryId: null, BioHubFacilityId: null, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;
        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);
        
        Either<ReadUserQueryResponse, Errors> result =
            await _readUserHandler.Handle(new(Id: id, OnlyCouriers: false, RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadCourierUser(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourierStaff);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;
        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ReadUserQueryResponse, Errors> result =
            await _readUserHandler.Handle(new(Id: id, OnlyCouriers: true, RoleType: null, LaboratoryId: null, BioHubFacilityId: null, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListUsersByLaboratoryIdForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid laboratoryId, Guid worklistToBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ListUsersByLaboratoryIdForWorklistToBioHubItemQueryResponse, Errors> result =
            await _readUserByLaboratoryIdForWorklistToBioHubItemHandler.Handle(new(LaboratoryId: laboratoryId, WorklistToBioHubItemId: worklistToBioHubItemId, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<UpdateUserCommand, Errors> body =
            await request.ParseBodyJson<UpdateUserCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<UpdateUserCommandResponse, Errors> result =
            await _updateUserHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> UpdateCourierUser(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditCourierStaff);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<UpdateUserCommand, Errors> body =
            await request.ParseBodyJson<UpdateUserCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<UpdateUserCommandResponse, Errors> result =
            await _updateUserHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> CreateFromUserRequest(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CreateUserFromUserRequestCommand, Errors> body =
            await request.ParseBodyJson<CreateUserFromUserRequestCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<CreateUserFromUserRequestCommandResponse, Errors> result =
            await _createUserFromUserRequestHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        string accessToken = GetAccessToken(request, log);

        if (!string.IsNullOrEmpty(accessToken))
        {
            var sendInvitationStatus = await _azureADUserInvitation.InviteUserAsync(body.Left.Email, accessToken, body.Left.FirstName, body.Left.LastName);
        }

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListUsersByBioHubFacilityIdForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid bioHubFacilityId, Guid worklistToBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;
        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ListUsersByBioHubFacilityIdForWorklistToBioHubItemQueryResponse, Errors> result =
            await _readUserByBioHubFacilityIdForWorklistToBioHubItemHandler.Handle(new(BioHubFacilityId: bioHubFacilityId, WorklistToBioHubItemId: worklistToBioHubItemId, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListCourierUsersForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<ListCourierUsersForWorklistToBioHubItemQueryResponse, Errors> result =
            await _readCourierUserForWorklistToBioHubItemHandler.Handle(new(WorklistToBioHubItemId: worklistToBioHubItemId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> ListUsersByLaboratoryIdForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid laboratoryId, Guid worklistFromBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();
        
        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryResponse, Errors> result =
            await _readUserByLaboratoryIdForWorklistFromBioHubItemHandler.Handle(new(LaboratoryId: laboratoryId, WorklistFromBioHubItemId: worklistFromBioHubItemId, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListUsersByBioHubFacilityIdForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid bioHubFacilityId, Guid worklistFromBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadUser);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryResponse, Errors> result =
            await _readUserByBioHubFacilityIdForWorklistFromBioHubItemHandler.Handle(new(BioHubFacilityId: bioHubFacilityId, WorklistFromBioHubItemId: worklistFromBioHubItemId, UserPermissions: userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListCourierUsersForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid worklistFromBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<ListCourierUsersForWorklistFromBioHubItemQueryResponse, Errors> result =
            await _readCourierUserForWorklistFromBioHubItemHandler.Handle(new(WorklistFromBioHubItemId: worklistFromBioHubItemId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
