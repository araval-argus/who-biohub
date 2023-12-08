using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.Shipments.ListShipments;
using WHO.BioHub.PublicData.Core.UseCases.Shipments.ReadShipment;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IShipmentsController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class ShipmentsController : ControllerBase, IShipmentsController
{
    private readonly IReadShipmentHandler _readShipmentHandler;
    private readonly IListShipmentsHandler _listShipmentsHandler;

    public ShipmentsController(
        IReadShipmentHandler readShipmentHandler,
        IListShipmentsHandler listShipmentsHandler)
    {
        _readShipmentHandler = readShipmentHandler;
        _listShipmentsHandler = listShipmentsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListShipmentsQueryResponse, Errors> result =
            await _listShipmentsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadShipmentQueryResponse, Errors> result =
            await _readShipmentHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
