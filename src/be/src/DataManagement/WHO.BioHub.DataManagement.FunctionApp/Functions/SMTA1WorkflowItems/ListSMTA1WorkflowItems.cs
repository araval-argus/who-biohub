using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.SMTA1WorkflowItems;

public class ListSMTA1WorkflowItems
{
    private readonly ISMTA1WorkflowItemsController _controller;

    public ListSMTA1WorkflowItems(ISMTA1WorkflowItemsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListSMTA1WorkflowItems))]
    [OpenApiOperation(operationId: "ListSMTA1WorkflowItem")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListSMTA1WorkflowItemsQueryResponse),
        Description = "The OK response")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.NotFound,
        contentType: "application/json",
        bodyType: typeof(Errors),
        Description = "Resource not found")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.BadRequest,
        contentType: "application/json",
        bodyType: typeof(Errors),
        Description = "Bad request")]
    public async Task<IActionResult> ListAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smta1workflowitems")] HttpRequest req,
        ILogger log,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("List SMTA1WorkflowItem function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.List(req, log, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing list SMTA1WorkflowItem function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
