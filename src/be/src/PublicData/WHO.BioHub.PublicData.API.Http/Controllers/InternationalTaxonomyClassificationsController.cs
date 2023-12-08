using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;
using WHO.BioHub.PublicData.Core.UseCases.InternationalTaxonomyClassifications.ReadInternationalTaxonomyClassification;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IInternationalTaxonomyClassificationsController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class InternationalTaxonomyClassificationsController : ControllerBase, IInternationalTaxonomyClassificationsController
{
    private readonly IReadInternationalTaxonomyClassificationHandler _readInternationalTaxonomyClassificationHandler;
    private readonly IListInternationalTaxonomyClassificationsHandler _listInternationalTaxonomyClassificationsHandler;

    public InternationalTaxonomyClassificationsController(
        IReadInternationalTaxonomyClassificationHandler readInternationalTaxonomyClassificationHandler,
        IListInternationalTaxonomyClassificationsHandler listInternationalTaxonomyClassificationsHandler)
    {
        _readInternationalTaxonomyClassificationHandler = readInternationalTaxonomyClassificationHandler;
        _listInternationalTaxonomyClassificationsHandler = listInternationalTaxonomyClassificationsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListInternationalTaxonomyClassificationsQueryResponse, Errors> result =
            await _listInternationalTaxonomyClassificationsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadInternationalTaxonomyClassificationQueryResponse, Errors> result =
            await _readInternationalTaxonomyClassificationHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
