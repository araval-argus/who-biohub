using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Search.API.Http.Controllers.FrontEndGlobalSearch;
using WHO.BioHub.Search.Core.UseCases.Aggregates.FrontEndGlobalSearch;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.FunctionApp.Functions.Entities.FrontEndGlobalSearch;

public class FrontEndGlobalSearch
{
    private readonly IFrontEndGlobalSearchController _controller;

    public FrontEndGlobalSearch(IFrontEndGlobalSearchController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(FrontEndGlobalSearch))]
    [OpenApiOperation(operationId: "FrontEndGlobalSearch")]
    [OpenApiRequestBody(
        contentType: "application/json",
        bodyType: typeof(FrontEndGlobalSearchQuery)
    )]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(FrontEndGlobalSearchQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "FrontEndGlobalSearch")] HttpRequest req,
        ILogger log,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Search FrontEndGlobalSearch function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.FrontEndGlobalSearch(req, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing search FrontEndGlobalSearch function");
            throw;
        }
    }
}
