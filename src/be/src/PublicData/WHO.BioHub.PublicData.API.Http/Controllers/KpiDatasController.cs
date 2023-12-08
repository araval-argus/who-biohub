using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.KpiDatas.ReadKpiData;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IKpiDatasController
{

    Task<IActionResult> KpiDatas(HttpRequest request, CancellationToken cancellationToken);
}

public class KpiDatasController : ControllerBase, IKpiDatasController
{
    private readonly IReadKpiDataHandler _readKpiDataHandler;

    public KpiDatasController(
        IReadKpiDataHandler readKpiDataHandler)
    {
        _readKpiDataHandler = readKpiDataHandler;
    }

    public async Task<IActionResult> KpiDatas(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ReadKpiDataQueryResponse, Errors> result =
            await _readKpiDataHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
