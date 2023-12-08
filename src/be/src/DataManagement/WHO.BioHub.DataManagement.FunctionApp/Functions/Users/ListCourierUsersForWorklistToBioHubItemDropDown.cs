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
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListCourierUsers;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.Users;

public class ListCourierUsersForWorklistToBioHubItemDropDown
{
    private readonly IUsersController _controller;

    public ListCourierUsersForWorklistToBioHubItemDropDown(IUsersController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListCourierUsersForWorklistToBioHubItemDropDown))]
    [OpenApiParameter(
        name: "worklistToBioHubItemId",
        Required = true,
        Description = "User LaboratoryId",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListCourierUsersForWorklistToBioHubItemQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/courier/{worklistToBioHubItemId:Guid}/worklistToBioHubItemId")] HttpRequest req,
        ILogger log,
        Guid worklistToBioHubItemId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Read User function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.ListCourierUsersForWorklistToBioHubItem(req, log, worklistToBioHubItemId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read user function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
