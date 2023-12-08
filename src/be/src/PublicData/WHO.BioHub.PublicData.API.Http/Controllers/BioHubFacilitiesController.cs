using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListBioHubFacilities;
using WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ReadBioHubFacility;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.PublicData.Core.UseCases.MapBioHubFacilities.ListMapBioHubFacilities;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IBioHubFacilitiesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> ListMap(HttpRequest request, CancellationToken cancellationToken);
}

public class BioHubFacilitiesController : ControllerBase, IBioHubFacilitiesController
{
    private readonly IReadBioHubFacilityHandler _readBioHubFacilityHandler;
    private readonly IListBioHubFacilitiesHandler _listBioHubFacilitiesHandler;
    private readonly IListMapBioHubFacilitiesHandler _listMapBioHubFacilitiesHandler;

    public BioHubFacilitiesController(
        IReadBioHubFacilityHandler readBioHubFacilityHandler,
        IListBioHubFacilitiesHandler listBioHubFacilitiesHandler,
        IListMapBioHubFacilitiesHandler listMapBioHubFacilitiesHandler)
    {
        _readBioHubFacilityHandler = readBioHubFacilityHandler;
        _listBioHubFacilitiesHandler = listBioHubFacilitiesHandler;
        _listMapBioHubFacilitiesHandler = listMapBioHubFacilitiesHandler;   
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListBioHubFacilitiesQueryResponse, Errors> result =
            await _listBioHubFacilitiesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListMap(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMapBioHubFacilitiesQueryResponse, Errors> result =
            await _listMapBioHubFacilitiesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadBioHubFacilityQueryResponse, Errors> result =
            await _readBioHubFacilityHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
