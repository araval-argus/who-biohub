using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItemEvents.ListWorklistFromBioHubItemEvents;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IWorklistFromBioHubItemEventsController
{
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistFromBioHubItemId, CancellationToken cancellationToken);
}

public class WorklistFromBioHubItemEventsController : BaseIdentityController, IWorklistFromBioHubItemEventsController
{
    private readonly IListWorklistFromBioHubItemEventsHandler _listWorklistFromBioHubItemEventsHandler;

    public WorklistFromBioHubItemEventsController(
        IListWorklistFromBioHubItemEventsHandler listWorklistFromBioHubItemEventsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _listWorklistFromBioHubItemEventsHandler = listWorklistFromBioHubItemEventsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistFromBioHubItemId, CancellationToken cancellationToken)
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

        Either<ListWorklistFromBioHubItemEventsQueryResponse, Errors> result =
            await _listWorklistFromBioHubItemEventsHandler.Handle(new(WorklistFromBioHubItemId: worklistFromBioHubItemId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
