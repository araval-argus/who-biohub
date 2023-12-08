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
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

using WHO.BioHub.Shared.Utils;
using System.Security.Claims;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.Materials;

public class ListMaterialsForWorklistToBioHubItemDropDown
{
    private readonly IMaterialsController _controller;

    public ListMaterialsForWorklistToBioHubItemDropDown(IMaterialsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListMaterialsForWorklistToBioHubItemDropDown))]
    [OpenApiParameter(
        name: "worklistToBioHubItemId",
        Required = true,
        Description = "worklistToBioHubItemId",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListMaterialsForWorklistToBioHubItemQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "materials/{worklistToBioHubItemId:Guid}/worklistToBioHubItemId")] HttpRequest req,
        ILogger log,
        Guid worklistToBioHubItemId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Read ListMaterialsForWorklistToBioHubItem function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.ListMaterialsForWorklistToBioHubItem(req, log, worklistToBioHubItemId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read ListMaterialsForWorklistToBioHubItem function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
