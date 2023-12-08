using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItemEvents.ListWorklistToBioHubItemEvents;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.WorklistToBioHubItemEvents;

public class ListWorklistToBioHubItemEvents
{
    private readonly IWorklistToBioHubItemEventsController _controller;

    public ListWorklistToBioHubItemEvents(IWorklistToBioHubItemEventsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListWorklistToBioHubItemEvents))]
    [OpenApiOperation(operationId: "ListWorklistToBioHubItemEvent")]
    [OpenApiParameter(
        name: "worklistToBioHubItemId",
        Required = true,
        Description = "WorklistToBioHubItem Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListWorklistToBioHubItemEventsQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "worklisttobiohubitemevents/{worklistToBioHubItemId:guid}")] HttpRequest req,
        ILogger log,
        Guid worklistToBioHubItemId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("List WorklistToBioHubItemEvent function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.List(req, log, worklistToBioHubItemId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing list worklisttobiohubitemevent function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
