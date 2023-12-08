using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.PublicData.Core.UseCases.Countries.ListCountries;
using WHO.BioHub.PublicData.Core.UseCases.Countries.ReadCountry;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface ICountriesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class CountriesController : ControllerBase, ICountriesController
{
    private readonly IReadCountryHandler _readCountryHandler;
    private readonly IListCountriesHandler _listCountriesHandler;

    public CountriesController(
        IReadCountryHandler readCountryHandler,
        IListCountriesHandler listCountriesHandler)
    {
        _readCountryHandler = readCountryHandler;
        _listCountriesHandler = listCountriesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListCountriesQueryResponse, Errors> result =
            await _listCountriesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadCountryQueryResponse, Errors> result =
            await _readCountryHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
