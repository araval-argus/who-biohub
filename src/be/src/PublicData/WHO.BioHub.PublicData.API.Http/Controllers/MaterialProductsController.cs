using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ListMaterialProducts;
using WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ReadMaterialProduct;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IMaterialProductsController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialProductsController : ControllerBase, IMaterialProductsController
{
    private readonly IReadMaterialProductHandler _readMaterialProductHandler;
    private readonly IListMaterialProductsHandler _listMaterialProductsHandler;

    public MaterialProductsController(
        IReadMaterialProductHandler readMaterialProductHandler,
        IListMaterialProductsHandler listMaterialProductsHandler)
    {
        _readMaterialProductHandler = readMaterialProductHandler;
        _listMaterialProductsHandler = listMaterialProductsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialProductsQueryResponse, Errors> result =
            await _listMaterialProductsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialProductQueryResponse, Errors> result =
            await _readMaterialProductHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
