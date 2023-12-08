using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Search.Core.UseCases.Laboratories.SearchLaboratoriesByCountry;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Search.API.Http.Controllers.SearchLaboratoriesByCountry;

public interface ISearchLaboratoriesByCountryController
{
    Task<IActionResult> SearchLaboratoriesByCountry(HttpRequest request, CancellationToken cancellationToken);
}

public class SearchLaboratoriesByCountryController : ControllerBase, ISearchLaboratoriesByCountryController
{
    private readonly ISearchLaboratoriesByCountryHandler _searchSearchLaboratoriesByCountryHandler;

    public SearchLaboratoriesByCountryController(
        ISearchLaboratoriesByCountryHandler searchSearchLaboratoriesByCountryHandler)
    {
        _searchSearchLaboratoriesByCountryHandler = searchSearchLaboratoriesByCountryHandler;
    }

    public async Task<IActionResult> SearchLaboratoriesByCountry(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<SearchLaboratoriesByCountryQuery, Errors> body =
            await request.ParseBodyJson<SearchLaboratoriesByCountryQuery>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<SearchLaboratoriesByCountryQueryResponse, Errors> result =
            await _searchSearchLaboratoriesByCountryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
