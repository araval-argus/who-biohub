using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Security.Claims;
using WHO.BioHub.DataManagement.API.Http.Controllers;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUserByExternalId;
using WHO.BioHub.Identity;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.FunctionApp.Functions.AuthChecks;

public class LoginCheck
{
    private readonly IAuthChecksController _controller;


    public LoginCheck(IAuthChecksController controller)
    {
        _controller = controller;
    }

    [FunctionName(nameof(LoginCheck))]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(GetAccessInformationQueryResponse),
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "auth")] HttpRequest req,
        ILogger log,
        CancellationToken funcCancellationToken,
        ClaimsPrincipal principal)
    {
        try
        {
            log.LogInformation("Get Access Information function (HttpTrigger) invoked.");

            CancellationTokenSource cancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(funcCancellationToken, req.HttpContext.RequestAborted);
            CancellationToken cancellationToken = cancellationSource.Token;

            IActionResult response = await _controller.LoginCheck(req, log, cancellationToken);
            return response;
        }

        catch (SecurityTokenExpiredException e)
        {
            log.LogError(e, "Token Expired");
            return new ObjectResult(new { Message = "Token Expired" })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        catch (Exception e)
        {
            log.LogError(e, "Error executing Get Access Information function");
            return new ObjectResult("Internal Server Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
