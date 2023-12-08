using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.SMTARequests.ListSMTARequests;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISMTARequestsController
{
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class SMTARequestsController : BaseIdentityController, ISMTARequestsController
{
    private readonly IListSMTARequestsHandler _listSMTARequestsHandler;

    public SMTARequestsController(
        IListSMTARequestsHandler listSMTARequestsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _listSMTARequestsHandler = listSMTARequestsHandler;
    }



    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<ListSMTARequestsQueryResponse, Errors> result =
            await _listSMTARequestsHandler.Handle(new(RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId, userPermissions), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
