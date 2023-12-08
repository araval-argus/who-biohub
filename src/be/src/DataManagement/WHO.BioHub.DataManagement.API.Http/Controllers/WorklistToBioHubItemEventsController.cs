using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItemEvents.ListWorklistToBioHubItemEvents;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IWorklistToBioHubItemEventsController
{
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken);
}

public class WorklistToBioHubItemEventsController : BaseIdentityController, IWorklistToBioHubItemEventsController
{
    private readonly IListWorklistToBioHubItemEventsHandler _listWorklistToBioHubItemEventsHandler;

    public WorklistToBioHubItemEventsController(
        IListWorklistToBioHubItemEventsHandler listWorklistToBioHubItemEventsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _listWorklistToBioHubItemEventsHandler = listWorklistToBioHubItemEventsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken)
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

        Either<ListWorklistToBioHubItemEventsQueryResponse, Errors> result =
            await _listWorklistToBioHubItemEventsHandler.Handle(new(WorklistToBioHubItemId: worklistToBioHubItemId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
