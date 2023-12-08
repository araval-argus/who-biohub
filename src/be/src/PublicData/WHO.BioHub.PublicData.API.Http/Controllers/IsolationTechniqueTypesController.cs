using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;
using WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ReadIsolationTechniqueType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IIsolationTechniqueTypesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class IsolationTechniqueTypesController : ControllerBase, IIsolationTechniqueTypesController
{
    private readonly IReadIsolationTechniqueTypeHandler _readIsolationTechniqueTypeHandler;
    private readonly IListIsolationTechniqueTypesHandler _listIsolationTechniqueTypesHandler;

    public IsolationTechniqueTypesController(
        IReadIsolationTechniqueTypeHandler readIsolationTechniqueTypeHandler,
        IListIsolationTechniqueTypesHandler listIsolationTechniqueTypesHandler)
    {
        _readIsolationTechniqueTypeHandler = readIsolationTechniqueTypeHandler;
        _listIsolationTechniqueTypesHandler = listIsolationTechniqueTypesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListIsolationTechniqueTypesQueryResponse, Errors> result =
            await _listIsolationTechniqueTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadIsolationTechniqueTypeQueryResponse, Errors> result =
            await _readIsolationTechniqueTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
