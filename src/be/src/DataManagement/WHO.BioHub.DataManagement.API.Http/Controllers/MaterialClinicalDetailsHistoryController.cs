using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.CreateMaterialClinicalDetailHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.DeleteMaterialClinicalDetailHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ListMaterialClinicalDetailsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ReadMaterialClinicalDetailHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.UpdateMaterialClinicalDetailHistory;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialClinicalDetailsHistoryController
{
    Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialClinicalDetailsHistoryController : ControllerBase, IMaterialClinicalDetailsHistoryController
{
    private readonly ICreateMaterialClinicalDetailHistoryHandler _createMaterialClinicalDetailHistoryHandler;
    private readonly IReadMaterialClinicalDetailHistoryHandler _readMaterialClinicalDetailHistoryHandler;
    private readonly IUpdateMaterialClinicalDetailHistoryHandler _updateMaterialClinicalDetailHistoryHandler;
    private readonly IDeleteMaterialClinicalDetailHistoryHandler _deleteMaterialClinicalDetailHistoryHandler;
    private readonly IListMaterialClinicalDetailsHistoryHandler _listMaterialClinicalDetailsHistoryHandler;

    public MaterialClinicalDetailsHistoryController(
        ICreateMaterialClinicalDetailHistoryHandler createMaterialClinicalDetailHistoryHandler,
        IReadMaterialClinicalDetailHistoryHandler readMaterialClinicalDetailHistoryHandler,
        IUpdateMaterialClinicalDetailHistoryHandler updateMaterialClinicalDetailHistoryHandler,
        IDeleteMaterialClinicalDetailHistoryHandler deleteMaterialClinicalDetailHistoryHandler,
        IListMaterialClinicalDetailsHistoryHandler listMaterialClinicalDetailsHistoryHandler)
    {
        _createMaterialClinicalDetailHistoryHandler = createMaterialClinicalDetailHistoryHandler;
        _readMaterialClinicalDetailHistoryHandler = readMaterialClinicalDetailHistoryHandler;
        _updateMaterialClinicalDetailHistoryHandler = updateMaterialClinicalDetailHistoryHandler;
        _deleteMaterialClinicalDetailHistoryHandler = deleteMaterialClinicalDetailHistoryHandler;
        _listMaterialClinicalDetailsHistoryHandler = listMaterialClinicalDetailsHistoryHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<CreateMaterialClinicalDetailHistoryCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialClinicalDetailHistoryCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateMaterialClinicalDetailHistoryCommandResponse, Errors> result =
            await _createMaterialClinicalDetailHistoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<DeleteMaterialClinicalDetailHistoryCommandResponse, Errors> result =
            await _deleteMaterialClinicalDetailHistoryHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialClinicalDetailsHistoryQueryResponse, Errors> result =
            await _listMaterialClinicalDetailsHistoryHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialClinicalDetailHistoryQueryResponse, Errors> result =
            await _readMaterialClinicalDetailHistoryHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<UpdateMaterialClinicalDetailHistoryCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialClinicalDetailHistoryCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateMaterialClinicalDetailHistoryCommandResponse, Errors> result =
            await _updateMaterialClinicalDetailHistoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
