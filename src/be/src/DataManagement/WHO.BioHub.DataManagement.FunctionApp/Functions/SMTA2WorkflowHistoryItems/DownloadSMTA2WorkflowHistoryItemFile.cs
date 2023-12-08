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
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DownloadSMTA2WorkflowHistoryItemFile;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.SMTA2WorkflowHistoryItems;

public class DownloadSMTA2WorkflowHistoryItemFile
{
    private readonly ISMTA2WorkflowHistoryItemsController _controller;

    public DownloadSMTA2WorkflowHistoryItemFile(ISMTA2WorkflowHistoryItemsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(DownloadSMTA2WorkflowHistoryItemFile))]
    [OpenApiParameter(
        name: "id",
        Required = true,
        Description = "Document Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiParameter(
        name: "worklistId",
        Required = true,
        Description = "Worklist Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(DownloadSMTA2WorkflowHistoryItemFileQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smta2workflowhistoryitems/{id:Guid}/downloadfile/{worklistId:Guid}/worklistid")] HttpRequest req,
        ILogger log,
        Guid id,
        Guid worklistId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Read SMTA2WorkflowHistoryItem function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.DownloadFile(req, log, id, worklistId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read smta2workflowitem function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
