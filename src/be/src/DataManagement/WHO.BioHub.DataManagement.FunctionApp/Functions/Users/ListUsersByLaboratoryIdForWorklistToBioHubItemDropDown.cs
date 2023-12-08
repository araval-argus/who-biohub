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
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.Users;

public class ListUsersByLaboratoryIdForWorklistToBioHubItemDropDown
{
    private readonly IUsersController _controller;

    public ListUsersByLaboratoryIdForWorklistToBioHubItemDropDown(IUsersController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListUsersByLaboratoryIdForWorklistToBioHubItemDropDown))]
    [OpenApiParameter(
        name: "laboratoryId",
        Required = true,
        Description = "User LaboratoryId",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiParameter(
        name: "worklistToBioHubItemId",
        Required = true,
        Description = "User LaboratoryId",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListUsersByLaboratoryIdForWorklistToBioHubItemQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{laboratoryId:Guid}/laboratoryId/{worklistToBioHubItemId:Guid}/worklistToBioHubItemId")] HttpRequest req,
        ILogger log,
        Guid laboratoryId,
        Guid worklistToBioHubItemId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Read User function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.ListUsersByLaboratoryIdForWorklistToBioHubItem(req, log, laboratoryId, worklistToBioHubItemId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read user function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
