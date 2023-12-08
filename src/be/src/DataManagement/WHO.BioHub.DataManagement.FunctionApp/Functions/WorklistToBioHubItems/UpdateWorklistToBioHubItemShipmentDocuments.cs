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
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItemShipmentDocuments;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.WorklistToBioHubItems;

public class UpdateWorklistToBioHubItemShipmentDocuments
{
    private readonly IWorklistToBioHubItemsController _controller;

    public UpdateWorklistToBioHubItemShipmentDocuments(IWorklistToBioHubItemsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(UpdateWorklistToBioHubItemShipmentDocuments))]
    [OpenApiOperation(operationId: "UpdateWorklistToBioHubItemShipmentDocuments")]
    [OpenApiParameter(
        name: "id",
        Required = true,
        Description = "WorklistToBioHubItem Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiRequestBody(
        contentType: "application/json",
        bodyType: typeof(UpdateWorklistToBioHubItemShipmentDocumentsCommand)
    )]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(UpdateWorklistToBioHubItemShipmentDocumentsCommandResponse),
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
    public async Task<IActionResult> UpdateAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "worklisttobiohubitems/{id:Guid}/shipmentdocuments")] HttpRequest req,
        ILogger log,
        Guid id,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Update WorklistToBioHubItem function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.UpdateShipmentDocuments(req, log, id, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing update worklisttobiohubitem function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
