using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;
using WHO.BioHub.PublicData.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface ICultivabilityTypesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class CultivabilityTypesController : ControllerBase, ICultivabilityTypesController
{
    private readonly IReadCultivabilityTypeHandler _readCultivabilityTypeHandler;
    private readonly IListCultivabilityTypesHandler _listCultivabilityTypesHandler;

    public CultivabilityTypesController(
        IReadCultivabilityTypeHandler readCultivabilityTypeHandler,
        IListCultivabilityTypesHandler listCultivabilityTypesHandler)
    {
        _readCultivabilityTypeHandler = readCultivabilityTypeHandler;
        _listCultivabilityTypesHandler = listCultivabilityTypesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListCultivabilityTypesQueryResponse, Errors> result =
            await _listCultivabilityTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadCultivabilityTypeQueryResponse, Errors> result =
            await _readCultivabilityTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
