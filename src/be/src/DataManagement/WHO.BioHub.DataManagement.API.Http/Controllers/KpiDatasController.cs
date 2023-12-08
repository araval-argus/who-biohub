using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Data.Core.UseCases.KpiDatas.ReadKpiData;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;
public interface IKpiDatasController
{
    Task<IActionResult> KpiDatas(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class KpiDatasController : BaseIdentityController, IKpiDatasController
{
    private readonly IReadKpiDataHandler _readKpiDataHandler;

    public KpiDatasController(
        IReadKpiDataHandler readKpiDataHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _readKpiDataHandler = readKpiDataHandler;
    }

    public async Task<IActionResult> KpiDatas(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadKpiData);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadKpiDataQueryResponse, Errors> result =
            await _readKpiDataHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
