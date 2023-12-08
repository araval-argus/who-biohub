using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;
using WHO.BioHub.PublicData.Core.UseCases.TemperatureUnitOfMeasures.ReadTemperatureUnitOfMeasure;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface ITemperatureUnitOfMeasuresController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class TemperatureUnitOfMeasuresController : ControllerBase, ITemperatureUnitOfMeasuresController
{
    private readonly IReadTemperatureUnitOfMeasureHandler _readTemperatureUnitOfMeasureHandler;
    private readonly IListTemperatureUnitOfMeasuresHandler _listTemperatureUnitOfMeasuresHandler;

    public TemperatureUnitOfMeasuresController(
        IReadTemperatureUnitOfMeasureHandler readTemperatureUnitOfMeasureHandler,
        IListTemperatureUnitOfMeasuresHandler listTemperatureUnitOfMeasuresHandler)
    {
        _readTemperatureUnitOfMeasureHandler = readTemperatureUnitOfMeasureHandler;
        _listTemperatureUnitOfMeasuresHandler = listTemperatureUnitOfMeasuresHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListTemperatureUnitOfMeasuresQueryResponse, Errors> result =
            await _listTemperatureUnitOfMeasuresHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadTemperatureUnitOfMeasureQueryResponse, Errors> result =
            await _readTemperatureUnitOfMeasureHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
