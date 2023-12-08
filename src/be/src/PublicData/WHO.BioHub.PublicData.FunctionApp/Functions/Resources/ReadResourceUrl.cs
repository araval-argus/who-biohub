using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using WHO.BioHub.PublicData.API.Http.Controllers;
using WHO.BioHub.PublicData.Core.UseCases.Resources.ReadResourceFileToken;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.FunctionApp.Functions.Resources;

public class ReadResourceFileToken
{
    private readonly IResourcesController _controller;

    public ReadResourceFileToken(IResourcesController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ReadResourceFileToken))]
    [OpenApiParameter(
        name: "id",
        Required = true,
        Description = "Resource Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ReadResourceFileTokenQueryResponse),
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
    public async Task<IActionResult> ReadFileTokenAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "resources/{id:Guid}/readfiletoken")] HttpRequest req,
        ILogger log,
        Guid id,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Read Resource function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.ReadFileToken(req, id, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read resource function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}