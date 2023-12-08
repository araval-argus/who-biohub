using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.MaterialTypes.ListMaterialTypes;
using WHO.BioHub.PublicData.Core.UseCases.MaterialTypes.ReadMaterialType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IMaterialTypesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialTypesController : ControllerBase, IMaterialTypesController
{
    private readonly IReadMaterialTypeHandler _readMaterialTypeHandler;
    private readonly IListMaterialTypesHandler _listMaterialTypesHandler;

    public MaterialTypesController(
        IReadMaterialTypeHandler readMaterialTypeHandler,
        IListMaterialTypesHandler listMaterialTypesHandler)
    {
        _readMaterialTypeHandler = readMaterialTypeHandler;
        _listMaterialTypesHandler = listMaterialTypesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialTypesQueryResponse, Errors> result =
            await _listMaterialTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialTypeQueryResponse, Errors> result =
            await _readMaterialTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
