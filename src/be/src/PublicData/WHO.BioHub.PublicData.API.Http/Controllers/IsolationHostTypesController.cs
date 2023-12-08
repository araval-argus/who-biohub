using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;
using WHO.BioHub.PublicData.Core.UseCases.IsolationHostTypes.ReadIsolationHostType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IIsolationHostTypesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class IsolationHostTypesController : ControllerBase, IIsolationHostTypesController
{
    private readonly IReadIsolationHostTypeHandler _readIsolationHostTypeHandler;
    private readonly IListIsolationHostTypesHandler _listIsolationHostTypesHandler;

    public IsolationHostTypesController(
        IReadIsolationHostTypeHandler readIsolationHostTypeHandler,
        IListIsolationHostTypesHandler listIsolationHostTypesHandler)
    {
        _readIsolationHostTypeHandler = readIsolationHostTypeHandler;
        _listIsolationHostTypesHandler = listIsolationHostTypesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListIsolationHostTypesQueryResponse, Errors> result =
            await _listIsolationHostTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadIsolationHostTypeQueryResponse, Errors> result =
            await _readIsolationHostTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
