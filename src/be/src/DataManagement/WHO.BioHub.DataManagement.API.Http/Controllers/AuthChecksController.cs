using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IAuthChecksController
{
    Task<IActionResult> CheckUserAccess(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> LoginCheck(HttpRequest request, ILogger log, CancellationToken cancellationToken);

}

public class AuthChecksController : BaseIdentityController, IAuthChecksController
{

    public AuthChecksController(IGetAccessInformationHandler getAccessInformationHandler, IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    { }

    public async Task<IActionResult> CheckUserAccess(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<GetAccessInformationQueryResponse, Errors> result =
            await base.CurrentUserLoginInfo(request, log, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> LoginCheck(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<GetAccessInformationQueryResponse, Errors> result =
            await base.CurrentUserLoginInfo(request, log, cancellationToken, true);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
