using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.Materials.ListMaterials;
using WHO.BioHub.PublicData.Core.UseCases.Materials.ReadMaterial;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IMaterialsController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialsController : ControllerBase, IMaterialsController
{
    private readonly IReadMaterialHandler _readMaterialHandler;
    private readonly IListMaterialsHandler _listMaterialsHandler;

    public MaterialsController(
        IReadMaterialHandler readMaterialHandler,
        IListMaterialsHandler listMaterialsHandler)
    {
        _readMaterialHandler = readMaterialHandler;
        _listMaterialsHandler = listMaterialsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialsQueryResponse, Errors> result =
            await _listMaterialsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialQueryResponse, Errors> result =
            await _readMaterialHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
