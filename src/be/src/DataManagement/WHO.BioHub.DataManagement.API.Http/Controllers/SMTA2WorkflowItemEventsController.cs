using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItemEvents.ListSMTA2WorkflowItemEvents;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISMTA2WorkflowItemEventsController
{
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken);
}

public class SMTA2WorkflowItemEventsController : BaseIdentityController, ISMTA2WorkflowItemEventsController
{
    private readonly IListSMTA2WorkflowItemEventsHandler _listSMTA2WorkflowItemEventsHandler;

    public SMTA2WorkflowItemEventsController(
        IListSMTA2WorkflowItemEventsHandler listSMTA2WorkflowItemEventsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _listSMTA2WorkflowItemEventsHandler = listSMTA2WorkflowItemEventsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListSMTA2WorkflowItemEventsQueryResponse, Errors> result =
            await _listSMTA2WorkflowItemEventsHandler.Handle(new(SMTA2WorkflowItemId: worklistToBioHubItemId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
