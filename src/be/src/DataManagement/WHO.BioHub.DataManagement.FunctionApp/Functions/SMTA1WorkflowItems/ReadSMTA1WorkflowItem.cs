using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.SMTA1WorkflowItems;

public class ReadSMTA1WorkflowItem
{
    private readonly ISMTA1WorkflowItemsController _controller;

    public ReadSMTA1WorkflowItem(ISMTA1WorkflowItemsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ReadSMTA1WorkflowItem))]
    [OpenApiParameter(
        name: "id",
        Required = true,
        Description = "SMTA1WorkflowItem Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ReadSMTA1WorkflowItemQueryResponse),
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
    public async Task<IActionResult> ReadAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smta1workflowitems/{id:Guid}")] HttpRequest req,
        ILogger log,
        Guid id,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Read SMTA1WorkflowItem function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.Read(req, log, id, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read SMTA1WorkflowItem function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
