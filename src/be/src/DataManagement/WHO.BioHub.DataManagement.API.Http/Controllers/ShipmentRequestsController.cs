using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.ShipmentRequests.ListShipmentRequests;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IShipmentRequestsController
{
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class ShipmentRequestsController : BaseIdentityController, IShipmentRequestsController
{
    private readonly IListShipmentRequestsHandler _listShipmentRequestsHandler;

    public ShipmentRequestsController(
        IListShipmentRequestsHandler listShipmentRequestsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _listShipmentRequestsHandler = listShipmentRequestsHandler;
    }



    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);        

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<ListShipmentRequestsQueryResponse, Errors> result =
            await _listShipmentRequestsHandler.Handle(new(RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId, userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
