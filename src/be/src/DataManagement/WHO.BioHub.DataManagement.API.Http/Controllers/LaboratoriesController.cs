using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.DeleteLaboratory;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListLaboratories;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadLaboratory;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratoryFromUserRequest;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListMapLaboratories;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ILaboratoriesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> CreateFromUserRequest(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ListMap(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class LaboratoriesController : BaseIdentityController, ILaboratoriesController
{
    private readonly ICreateLaboratoryHandler _createLaboratoryHandler;
    private readonly IReadLaboratoryHandler _readLaboratoryHandler;
    private readonly IUpdateLaboratoryHandler _updateLaboratoryHandler;
    private readonly IDeleteLaboratoryHandler _deleteLaboratoryHandler;
    private readonly IListLaboratoriesHandler _listLaboratoriesHandler;
    private readonly IListMapLaboratoriesHandler _listMapLaboratoriesHandler;
    private readonly ICreateLaboratoryFromUserRequestHandler _createLaboratoryFromUserRequestHandler;

    public LaboratoriesController(
        ICreateLaboratoryHandler createLaboratoryHandler,
        IReadLaboratoryHandler readLaboratoryHandler,
        IUpdateLaboratoryHandler updateLaboratoryHandler,
        IDeleteLaboratoryHandler deleteLaboratoryHandler,
        IListLaboratoriesHandler listLaboratoriesHandler,
        IListMapLaboratoriesHandler listMapLaboratoriesHandler,
        ICreateLaboratoryFromUserRequestHandler createLaboratoryFromUserRequestHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createLaboratoryHandler = createLaboratoryHandler;
        _readLaboratoryHandler = readLaboratoryHandler;
        _updateLaboratoryHandler = updateLaboratoryHandler;
        _deleteLaboratoryHandler = deleteLaboratoryHandler;
        _listLaboratoriesHandler = listLaboratoriesHandler;
        _listMapLaboratoriesHandler = listMapLaboratoriesHandler;
        _createLaboratoryFromUserRequestHandler = createLaboratoryFromUserRequestHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateLaboratory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        
        var userLoginInfo = checkUserPermissionResult.Left;
        if (userLoginInfo.RoleType != RoleType.WHO)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<CreateLaboratoryCommand, Errors> body =
            await request.ParseBodyJson<CreateLaboratoryCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<CreateLaboratoryCommandResponse, Errors> result =
            await _createLaboratoryHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteLaboratory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();
       
        var userLoginInfo = checkUserPermissionResult.Left;
        if (userLoginInfo.RoleType != RoleType.WHO)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<DeleteLaboratoryCommandResponse, Errors> result =
            await _deleteLaboratoryHandler.Handle(new(Id: id, OperationById: checkUserPermissionResult.Left.UserId), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadLaboratory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListLaboratoriesQueryResponse, Errors> result =
            await _listLaboratoriesHandler.Handle(new(RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListMap(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadLaboratory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListMapLaboratoriesQueryResponse, Errors> result =
            await _listMapLaboratoriesHandler.Handle(new(RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadLaboratory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;


        Either<ReadLaboratoryQueryResponse, Errors> result =
            await _readLaboratoryHandler.Handle(new(Id: id, RoleType: userLoginInfo.RoleType, UserLaboratoryId: userLoginInfo.LaboratoryId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditLaboratory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        if (!(userLoginInfo.RoleType == RoleType.WHO || (userLoginInfo.RoleType == RoleType.Laboratory && userLoginInfo.LaboratoryId == id)))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        var operationById = checkUserPermissionResult.Left.UserId;

        Either<UpdateLaboratoryCommand, Errors> body =
            await request.ParseBodyJson<UpdateLaboratoryCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;
        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<UpdateLaboratoryCommandResponse, Errors> result =
            await _updateLaboratoryHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> CreateFromUserRequest(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateLaboratory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        if (userLoginInfo.RoleType != RoleType.WHO)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }
        

        Either<CreateLaboratoryFromUserRequestCommand, Errors> body =
            await request.ParseBodyJson<CreateLaboratoryFromUserRequestCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;
        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<CreateLaboratoryFromUserRequestCommandResponse, Errors> result =
            await _createLaboratoryFromUserRequestHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
