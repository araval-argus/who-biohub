using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.CheckDocumentSigned;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.Documents;

public class CheckDocument
{
    private readonly IDocumentsController _controller;

    public CheckDocument(IDocumentsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(CheckDocument))]
    [OpenApiParameter(
        name: "documentType",
        Required = true,
        Description = "Document Type",
        Type = typeof(int),
        In = ParameterLocation.Path)]
    [OpenApiParameter(
        name: "laboratoryId",
        Required = true,
        Description = "laboratory Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(CheckDocumentSignedQueryResponse),
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
    public async Task<IActionResult> CheckAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "documents/{documentType:int}/type/{laboratoryId:guid}/laboratory/check")] HttpRequest req,
        ILogger log,
        int documentType,
        Guid laboratoryId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Check Document function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.CheckDocumentSigned(req, log, (DocumentFileType)documentType, laboratoryId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read document function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
