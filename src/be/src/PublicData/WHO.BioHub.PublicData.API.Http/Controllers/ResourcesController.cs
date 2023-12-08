using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.Resources.ListResources;
using WHO.BioHub.PublicData.Core.UseCases.Resources.ReadResourceFileToken;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IResourcesController
{
    Task<IActionResult> List(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadFileToken(HttpRequest request, Guid id, CancellationToken cancellationToken);
}

public class ResourcesController : ControllerBase, IResourcesController
{
    private readonly IReadResourceFileTokenHandler _readResourceFileTokenHandler;
    private readonly IListResourcesHandler _listResourcesHandler;

    public ResourcesController(
        IReadResourceFileTokenHandler readResourceFileTokenHandler,
        IListResourcesHandler listResourcesHandler)
    {
        _readResourceFileTokenHandler = readResourceFileTokenHandler;
        _listResourcesHandler = listResourcesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ListResourcesQueryResponse, Errors> result =
            await _listResourcesHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadFileToken(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {      

        Either<ReadResourceFileTokenQueryResponse, Errors> result =
            await _readResourceFileTokenHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
