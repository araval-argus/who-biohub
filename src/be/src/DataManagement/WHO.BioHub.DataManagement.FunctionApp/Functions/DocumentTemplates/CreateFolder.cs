using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CreateFolder;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.DocumentTemplates;

public class CreateFolder
{
    private readonly IDocumentTemplatesController _controller;

    public CreateFolder(IDocumentTemplatesController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(CreateFolder))]
    [OpenApiOperation(operationId: "CreateFolder")]
    [OpenApiRequestBody(
        contentType: "application/json",
        bodyType: typeof(CreateFolderCommand)
    )]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(CreateFolderCommandResponse),
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
    public async Task<IActionResult> CreateAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "documenttemplates/createfolder")] HttpRequest req,
        ILogger log,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("DocumentTemplate create folder function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.CreateFolder(req, log, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing documenttemplate create folder function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
