using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListLaboratories;
using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ReadLaboratory;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListMapLaboratories;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface ILaboratoriesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> ListMap(HttpRequest request, CancellationToken cancellationToken);
}

public class LaboratoriesController : ControllerBase, ILaboratoriesController
{
    private readonly IReadLaboratoryHandler _readLaboratoryHandler;
    private readonly IListLaboratoriesHandler _listLaboratoriesHandler;
    private readonly IListMapLaboratoriesHandler _listMapLaboratoriesHandler;

    public LaboratoriesController(
        IReadLaboratoryHandler readLaboratoryHandler,
        IListLaboratoriesHandler listLaboratoriesHandler,
        IListMapLaboratoriesHandler listMapLaboratoriesHandler)
    {
        _readLaboratoryHandler = readLaboratoryHandler;
        _listLaboratoriesHandler = listLaboratoriesHandler;
        _listMapLaboratoriesHandler = listMapLaboratoriesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListLaboratoriesQueryResponse, Errors> result =
            await _listLaboratoriesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListMap(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMapLaboratoriesQueryResponse, Errors> result =
            await _listMapLaboratoriesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadLaboratoryQueryResponse, Errors> result =
            await _readLaboratoryHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
