using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Shared.Dto;
using System.Security.Claims;
using WHO.BioHub.Identity;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Graph;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public class BaseIdentityController : ControllerBase
{

    private readonly IGetAccessInformationHandler _getAccessInformationHandler;
    private readonly IAzureADTokenValidation _azureADTokenValidation;

    public BaseIdentityController(IGetAccessInformationHandler getAccessInformationHandler, IAzureADTokenValidation azureADTokenValidation)
    {
        _getAccessInformationHandler = getAccessInformationHandler;
        _azureADTokenValidation = azureADTokenValidation;
    }

    private Guid? ExternalId(ClaimsPrincipal principal)
    {
        string externalIdString = principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
        if (Guid.TryParse(externalIdString, out Guid externalId))
        {
            return externalId;
        }

        return null;
    }

    private string? Email(ClaimsPrincipal principal)
    {
        var email = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        if (email == null)
        {
            email = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value;
        }

        return email;
    }

    public async Task<Either<GetAccessInformationQueryResponse, Errors>> CurrentUserLoginInfo(HttpRequest request, ILogger log, CancellationToken cancellationToken, bool isLoginCheck = false)
    {
        var principal = await CheckPrincipal(request, log, cancellationToken);

        if (principal == null || principal.Claims.Count() == 0)
        {
            UserLoginInfo userLoginInfo = new UserLoginInfo();

            userLoginInfo.RoleType = RoleType.Public;
            userLoginInfo.LandingPage = "publicarea";
            userLoginInfo.UserLogged = false;

            return new(new GetAccessInformationQueryResponse(userLoginInfo));
        }

        var externalId = ExternalId(principal);
        var email = Email(principal);

        var result = await _getAccessInformationHandler.Handle(new(ExternalId: externalId, Email: email, IsLoginCheck: isLoginCheck), cancellationToken);

        return result;

    }

    private async Task<ClaimsPrincipal> CheckPrincipal(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        ClaimsPrincipal principal = null;
        ObjectResult claimsPrincipalActionResult = await _azureADTokenValidation.ValidateRequestAsync(request, log, cancellationToken);
        if (claimsPrincipalActionResult.StatusCode == StatusCodes.Status200OK && claimsPrincipalActionResult.Value != null)
        {
            principal = (ClaimsPrincipal)claimsPrincipalActionResult?.Value;
        }


        return principal;
    }

    public string GetAccessToken(HttpRequest request, ILogger log)
    {
        return _azureADTokenValidation.GetAccessToken(request, log);
    }


    public async Task<Either<UserLoginInfo, Errors>> CheckUserPermission(HttpRequest request, ILogger log, CancellationToken cancellationToken, string? permission = null)
    {
        Either<GetAccessInformationQueryResponse, Errors> identityCheckResult =
            await CurrentUserLoginInfo(request, log, cancellationToken);
        if (identityCheckResult.IsRight)
            return new(new Errors(ErrorType.Unauthorized, "unauthorized"));

        var userLoginInfo = identityCheckResult.Left.UserLoginInfo;

        if (userLoginInfo.RoleType == RoleType.Public)
        {
            return new(new Errors(ErrorType.Unauthorized, "unauthorized"));
        }

        if (!string.IsNullOrEmpty(permission) && !userLoginInfo.UserPermissions.Select(x => x.PermissionName).Contains(permission))
        {
            return new(new Errors(ErrorType.Unauthorized, "unauthorized"));
        }

        return new(userLoginInfo);
    }

}
