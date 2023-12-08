using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.UserRequests;

public class UpdateUserRequest
{
    private readonly IUserRequestsController _controller;

    public UpdateUserRequest(IUserRequestsController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(UpdateUserRequest))]
    [OpenApiOperation(operationId: "UpdateUserRequest")]
    [OpenApiParameter(
        name: "id",
        Required = true,
        Description = "UserRequest Id",
        Type = typeof(Guid),
        In = ParameterLocation.Path)]
    [OpenApiRequestBody(
        contentType: "application/json",
        bodyType: typeof(UpdateUserRequestCommand)
    )]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(UpdateUserRequestCommandResponse),
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
    public async Task<IActionResult> UpdateAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "userrequests/{id:Guid}")] HttpRequest req,
        ILogger log,
        Guid id,
        CancellationToken funcCancellationToken)
    {
        try
        {
            log.LogInformation("Update UserRequest function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.Update(req, log, id, cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error executing update userRequest function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
