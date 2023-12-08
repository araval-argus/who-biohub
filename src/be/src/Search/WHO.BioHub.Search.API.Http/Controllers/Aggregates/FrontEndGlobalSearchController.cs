using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Search.Core.UseCases.Aggregates.FrontEndGlobalSearch;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.API.Http.Controllers.FrontEndGlobalSearch;

public interface IFrontEndGlobalSearchController
{
    Task<IActionResult> FrontEndGlobalSearch(HttpRequest request, CancellationToken cancellationToken);
}

public class FrontEndGlobalSearchController : ControllerBase, IFrontEndGlobalSearchController
{
    private readonly IFrontEndGlobalSearchHandler _searchFrontEndGlobalSearchHandler;

    public FrontEndGlobalSearchController(
        IFrontEndGlobalSearchHandler searchFrontEndGlobalSearchHandler)
    {
        _searchFrontEndGlobalSearchHandler = searchFrontEndGlobalSearchHandler;
    }

    public async Task<IActionResult> FrontEndGlobalSearch(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<FrontEndGlobalSearchQuery, Errors> body =
            await request.ParseBodyJson<FrontEndGlobalSearchQuery>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<FrontEndGlobalSearchQueryResponse, Errors> result =
            await _searchFrontEndGlobalSearchHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
