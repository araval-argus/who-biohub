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
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.Users;

public class ListUsersByBioHubFacilityIdForWorklistFromBioHubItemDropDown
{
    private readonly IUsersController _controller;

    public ListUsersByBioHubFacilityIdForWorklistFromBioHubItemDropDown(IUsersController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(ListUsersByBioHubFacilityIdForWorklistFromBioHubItemDropDown))]
    [OpenApiParameter(
        name: "bioHubFacilityId",
        Required = true,
        Description = "User BioHubFacilityId",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiParameter(
        name: "worklistFromBioHubItemId",
        Required = true,
        Description = "User BioHubFacilityId",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{bioHubFacilityId:Guid}/bioHubFacilityId/{worklistFromBioHubItemId:Guid}/worklistFromBioHubItemId")] HttpRequest req,
        ILogger log,
        Guid bioHubFacilityId,
        Guid worklistFromBioHubItemId,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Read User function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.ListUsersByBioHubFacilityIdForWorklistFromBioHubItem(req, log, bioHubFacilityId, worklistFromBioHubItemId, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing read user function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
