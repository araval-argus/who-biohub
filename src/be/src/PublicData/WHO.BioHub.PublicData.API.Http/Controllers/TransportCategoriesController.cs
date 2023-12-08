using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ListTransportCategories;
using WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ReadTransportCategory;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface ITransportCategoriesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class TransportCategoriesController : ControllerBase, ITransportCategoriesController
{
    private readonly IReadTransportCategoryHandler _readTransportCategoryHandler;
    private readonly IListTransportCategoriesHandler _listTransportCategoriesHandler;

    public TransportCategoriesController(
        IReadTransportCategoryHandler readTransportCategoryHandler,
        IListTransportCategoriesHandler listTransportCategoriesHandler)
    {
        _readTransportCategoryHandler = readTransportCategoryHandler;
        _listTransportCategoriesHandler = listTransportCategoriesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListTransportCategoriesQueryResponse, Errors> result =
            await _listTransportCategoriesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadTransportCategoryQueryResponse, Errors> result =
            await _readTransportCategoryHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
