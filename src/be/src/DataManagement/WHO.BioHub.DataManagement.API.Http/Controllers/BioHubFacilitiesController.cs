using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.CreateBioHubFacility;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.DeleteBioHubFacility;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ReadBioHubFacility;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.UpdateBioHubFacility;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IBioHubFacilitiesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ListMap(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class BioHubFacilitiesController : BaseIdentityController, IBioHubFacilitiesController
{
    private readonly ICreateBioHubFacilityHandler _createBioHubFacilityHandler;
    private readonly IReadBioHubFacilityHandler _readBioHubFacilityHandler;
    private readonly IUpdateBioHubFacilityHandler _updateBioHubFacilityHandler;
    private readonly IDeleteBioHubFacilityHandler _deleteBioHubFacilityHandler;
    private readonly IListBioHubFacilitiesHandler _listBioHubFacilitiesHandler;
    private readonly IListMapBioHubFacilitiesHandler _listMapBioHubFacilitiesHandler;


    public BioHubFacilitiesController(
        ICreateBioHubFacilityHandler createBioHubFacilityHandler,
        IReadBioHubFacilityHandler readBioHubFacilityHandler,
        IUpdateBioHubFacilityHandler updateBioHubFacilityHandler,
        IDeleteBioHubFacilityHandler deleteBioHubFacilityHandler,
        IListBioHubFacilitiesHandler listBioHubFacilitiesHandler,
        IListMapBioHubFacilitiesHandler listMapBioHubFacilitiesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createBioHubFacilityHandler = createBioHubFacilityHandler;
        _readBioHubFacilityHandler = readBioHubFacilityHandler;
        _updateBioHubFacilityHandler = updateBioHubFacilityHandler;
        _deleteBioHubFacilityHandler = deleteBioHubFacilityHandler;
        _listBioHubFacilitiesHandler = listBioHubFacilitiesHandler;
        _listMapBioHubFacilitiesHandler = listMapBioHubFacilitiesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {

        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateBioHubFacility);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CreateBioHubFacilityCommand, Errors> body =
            await request.ParseBodyJson<CreateBioHubFacilityCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<CreateBioHubFacilityCommandResponse, Errors> result =
            await _createBioHubFacilityHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteBioHubFacility);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        
        var userLoginInfo = checkUserPermissionResult.Left;
        if (userLoginInfo.RoleType != RoleType.WHO)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<DeleteBioHubFacilityCommandResponse, Errors> result =
            await _deleteBioHubFacilityHandler.Handle(new(Id: id, OperationById: checkUserPermissionResult.Left.UserId), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadBioHubFacility);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListBioHubFacilitiesQueryResponse, Errors> result =
            await _listBioHubFacilitiesHandler.Handle(new(RoleType: userLoginInfo.RoleType), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListMap(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadBioHubFacility);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListMapBioHubFacilitiesQueryResponse, Errors> result =
            await _listMapBioHubFacilitiesHandler.Handle(new(RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadBioHubFacility);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<ReadBioHubFacilityQueryResponse, Errors> result =
            await _readBioHubFacilityHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditBioHubFacility);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateBioHubFacilityCommand, Errors> body =
            await request.ParseBodyJson<UpdateBioHubFacilityCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;

        command.OperationById = checkUserPermissionResult.Left.UserId;

        Either<UpdateBioHubFacilityCommandResponse, Errors> result =
            await _updateBioHubFacilityHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }



}
