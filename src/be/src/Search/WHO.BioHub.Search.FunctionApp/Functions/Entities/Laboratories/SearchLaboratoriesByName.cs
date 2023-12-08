using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Search.API.Http.Controllers.SearchLaboratoriesByName;
using WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByName;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.FunctionApp.Functions.Entities.Laboratories;

public class SearchLaboratoriesByName
{
    private readonly ISearchLaboratoriesByNameController _controller;

    public SearchLaboratoriesByName(ISearchLaboratoriesByNameController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(SearchLaboratoriesByName))]
    [OpenApiOperation(operationId: "SearchLaboratoriesByName")]
    [OpenApiRequestBody(
        contentType: "application/json",
        bodyType: typeof(SearchLaboratoriesByNameQuery)
    )]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(SearchLaboratoriesByNameQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Laboratory/Name")] HttpRequest req,
        ILogger log,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Search SearchLaboratoriesByName (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.SearchLaboratoriesByName(req, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing search SearchLaboratoriesByName function");
            throw;
        }
    }
}
