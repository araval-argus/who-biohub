using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;
using WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ReadGeneticSequenceData;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IGeneticSequenceDatasController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class GeneticSequenceDatasController : ControllerBase, IGeneticSequenceDatasController
{
    private readonly IReadGeneticSequenceDataHandler _readGeneticSequenceDataHandler;
    private readonly IListGeneticSequenceDatasHandler _listGeneticSequenceDatasHandler;

    public GeneticSequenceDatasController(
        IReadGeneticSequenceDataHandler readGeneticSequenceDataHandler,
        IListGeneticSequenceDatasHandler listGeneticSequenceDatasHandler)
    {
        _readGeneticSequenceDataHandler = readGeneticSequenceDataHandler;
        _listGeneticSequenceDatasHandler = listGeneticSequenceDatasHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListGeneticSequenceDatasQueryResponse, Errors> result =
            await _listGeneticSequenceDatasHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadGeneticSequenceDataQueryResponse, Errors> result =
            await _readGeneticSequenceDataHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
