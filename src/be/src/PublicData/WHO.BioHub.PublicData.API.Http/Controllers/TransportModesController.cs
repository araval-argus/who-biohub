using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.TransportModes.ListTransportModes;
using WHO.BioHub.PublicData.Core.UseCases.TransportModes.ReadTransportMode;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface ITransportModesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class TransportModesController : ControllerBase, ITransportModesController
{
    private readonly IReadTransportModeHandler _readTransportModeHandler;
    private readonly IListTransportModesHandler _listTransportModesHandler;

    public TransportModesController(
        IReadTransportModeHandler readTransportModeHandler,
        IListTransportModesHandler listTransportModesHandler)
    {
        _readTransportModeHandler = readTransportModeHandler;
        _listTransportModesHandler = listTransportModesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListTransportModesQueryResponse, Errors> result =
            await _listTransportModesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadTransportModeQueryResponse, Errors> result =
            await _readTransportModeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
