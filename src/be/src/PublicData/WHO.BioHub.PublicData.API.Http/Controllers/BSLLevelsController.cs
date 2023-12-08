using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.PublicData.Core.UseCases.BSLLevels.ListBSLLevels;
using WHO.BioHub.PublicData.Core.UseCases.BSLLevels.ReadBSLLevel;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IBSLLevelsController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class BSLLevelsController : ControllerBase, IBSLLevelsController
{
    private readonly IReadBSLLevelHandler _readBSLLevelHandler;
    private readonly IListBSLLevelsHandler _listBSLLevelsHandler;

    public BSLLevelsController(
        IReadBSLLevelHandler readBSLLevelHandler,
        IListBSLLevelsHandler listBSLLevelsHandler)
    {
        _readBSLLevelHandler = readBSLLevelHandler;
        _listBSLLevelsHandler = listBSLLevelsHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListBSLLevelsQueryResponse, Errors> result =
            await _listBSLLevelsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadBSLLevelQueryResponse, Errors> result =
            await _readBSLLevelHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
