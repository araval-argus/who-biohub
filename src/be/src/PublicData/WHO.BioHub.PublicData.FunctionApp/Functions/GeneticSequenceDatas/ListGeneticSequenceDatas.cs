using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using WHO.BioHub.PublicData.API.Http.Controllers;
using WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.FunctionApp.Functions.GeneticSequenceDatas;

public class ListGeneticSequenceDatas
{
    private readonly IGeneticSequenceDatasController _controller;

    public ListGeneticSequenceDatas(IGeneticSequenceDatasController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListGeneticSequenceDatas))]
    [OpenApiOperation(operationId: "ListGeneticSequenceData")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListGeneticSequenceDatasQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "geneticsequencedatas")] HttpRequest req,
        ILogger log,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("List GeneticSequenceData function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.List(req, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing list geneticsequencedata function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
