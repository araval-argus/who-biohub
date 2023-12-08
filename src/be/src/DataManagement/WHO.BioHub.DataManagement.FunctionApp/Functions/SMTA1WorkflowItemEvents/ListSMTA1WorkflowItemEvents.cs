using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItemEvents.ListSMTA1WorkflowItemEvents;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.SMTA1WorkflowItemEvents;

public class ListSMTA1WorkflowItemEvents
{
    private readonly ISMTA1WorkflowItemEventsController _controller;

    public ListSMTA1WorkflowItemEvents(ISMTA1WorkflowItemEventsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListSMTA1WorkflowItemEvents))]
    [OpenApiOperation(operationId: "ListSMTA1WorkflowItemEvent")]
    [OpenApiParameter(
        name: "SMTA1WorkflowItemId",
        Required = true,
        Description = "SMTA1WorkflowItem Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListSMTA1WorkflowItemEventsQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smta1workflowitemevents/{SMTA1WorkflowItemId:guid}")] HttpRequest req,
        ILogger log,
        Guid SMTA1WorkflowItemId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("List SMTA1WorkflowItemEvent function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.List(req, log, SMTA1WorkflowItemId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing list SMTA1WorkflowItemEvent function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
