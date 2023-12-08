using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByName;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.API.Http.Controllers.SearchLaboratoriesByName;

public interface ISearchLaboratoriesByNameController
{
    Task<IActionResult> SearchLaboratoriesByName(HttpRequest request, CancellationToken cancellationToken);
}

public class SearchLaboratoriesByNameController : ControllerBase, ISearchLaboratoriesByNameController
{
    private readonly ISearchLaboratoriesByNameHandler _searchSearchLaboratoriesByNameHandler;

    public SearchLaboratoriesByNameController(
        ISearchLaboratoriesByNameHandler searchSearchLaboratoriesByNameHandler)
    {
        _searchSearchLaboratoriesByNameHandler = searchSearchLaboratoriesByNameHandler;
    }

    public async Task<IActionResult> SearchLaboratoriesByName(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<SearchLaboratoriesByNameQuery, Errors> body =
            await request.ParseBodyJson<SearchLaboratoriesByNameQuery>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<SearchLaboratoriesByNameQueryResponse, Errors> result =
            await _searchSearchLaboratoriesByNameHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
