using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;
using WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ReadPriorityRequestType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IPriorityRequestTypesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class PriorityRequestTypesController : ControllerBase, IPriorityRequestTypesController
{
    private readonly IReadPriorityRequestTypeHandler _readPriorityRequestTypeHandler;
    private readonly IListPriorityRequestTypesHandler _listPriorityRequestTypesHandler;

    public PriorityRequestTypesController(
        IReadPriorityRequestTypeHandler readPriorityRequestTypeHandler,
        IListPriorityRequestTypesHandler listPriorityRequestTypesHandler)
    {
        _readPriorityRequestTypeHandler = readPriorityRequestTypeHandler;
        _listPriorityRequestTypesHandler = listPriorityRequestTypesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListPriorityRequestTypesQueryResponse, Errors> result =
            await _listPriorityRequestTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadPriorityRequestTypeQueryResponse, Errors> result =
            await _readPriorityRequestTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
