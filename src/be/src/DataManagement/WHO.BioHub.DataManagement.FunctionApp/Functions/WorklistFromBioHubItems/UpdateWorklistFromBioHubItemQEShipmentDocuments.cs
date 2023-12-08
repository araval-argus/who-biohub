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
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemQEShipmentDocuments;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.WorklistFromBioHubItems;

public class UpdateWorklistFromBioHubItemQEShipmentDocuments
{
    private readonly IWorklistFromBioHubItemsController _controller;

    public UpdateWorklistFromBioHubItemQEShipmentDocuments(IWorklistFromBioHubItemsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(UpdateWorklistFromBioHubItemQEShipmentDocuments))]
    [OpenApiOperation(operationId: "UpdateWorklistFromBioHubItemQEShipmentDocuments")]
    [OpenApiParameter(
        name: "id",
        Required = true,
        Description = "WorklistFromBioHubItem Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiRequestBody(
        contentType: "application/json",
        bodyType: typeof(UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand)
    )]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(UpdateWorklistFromBioHubItemQEShipmentDocumentsCommandResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "worklistfrombiohubitems/{id:Guid}/qeshipmentdocuments")] HttpRequest req,
        ILogger log,
        Guid id,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Update WorklistFromBioHubItem function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.UpdateQEShipmentDocuments(req, log, id, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing update worklistfrombiohubitem function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
