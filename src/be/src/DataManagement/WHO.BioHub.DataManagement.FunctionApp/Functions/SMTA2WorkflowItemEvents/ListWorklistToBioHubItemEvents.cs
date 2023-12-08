using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItemEvents.ListSMTA2WorkflowItemEvents;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.SMTA2WorkflowItemEvents;

public class ListSMTA2WorkflowItemEvents
{
    private readonly ISMTA2WorkflowItemEventsController _controller;

    public ListSMTA2WorkflowItemEvents(ISMTA2WorkflowItemEventsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListSMTA2WorkflowItemEvents))]
    [OpenApiOperation(operationId: "ListSMTA2WorkflowItemEvent")]
    [OpenApiParameter(
        name: "SMTA2WorkflowItemId",
        Required = true,
        Description = "SMTA2WorkflowItem Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListSMTA2WorkflowItemEventsQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smta2workflowitemevents/{SMTA2WorkflowItemId:guid}")] HttpRequest req,
        ILogger log,
        Guid SMTA2WorkflowItemId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("List SMTA2WorkflowItemEvent function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.List(req, log, SMTA2WorkflowItemId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing list SMTA2WorkflowItemEvent function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
