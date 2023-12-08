using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItemEvents.ListSMTA1WorkflowItemEvents;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISMTA1WorkflowItemEventsController
{
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken);
}

public class SMTA1WorkflowItemEventsController : BaseIdentityController, ISMTA1WorkflowItemEventsController
{
    private readonly IListSMTA1WorkflowItemEventsHandler _listSMTA1WorkflowItemEventsHandler;

    public SMTA1WorkflowItemEventsController(
        IListSMTA1WorkflowItemEventsHandler listSMTA1WorkflowItemEventsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _listSMTA1WorkflowItemEventsHandler = listSMTA1WorkflowItemEventsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListSMTA1WorkflowItemEventsQueryResponse, Errors> result =
            await _listSMTA1WorkflowItemEventsHandler.Handle(new(SMTA1WorkflowItemId: worklistToBioHubItemId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
