using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.CreateMaterialShippingInformationHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.DeleteMaterialShippingInformationHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ListMaterialShippingInformationsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ReadMaterialShippingInformationHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.UpdateMaterialShippingInformationHistory;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ReadMaterialShippingInformation;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialShippingInformationsHistoryController
{
    Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialShippingInformationsHistoryController : ControllerBase, IMaterialShippingInformationsHistoryController
{
    private readonly ICreateMaterialShippingInformationHistoryHandler _createMaterialShippingInformationHistoryHandler;
    private readonly IReadMaterialShippingInformationHistoryHandler _readMaterialShippingInformationHistoryHandler;
    private readonly IUpdateMaterialShippingInformationHistoryHandler _updateMaterialShippingInformationHistoryHandler;
    private readonly IDeleteMaterialShippingInformationHistoryHandler _deleteMaterialShippingInformationHistoryHandler;
    private readonly IListMaterialShippingInformationsHistoryHandler _listMaterialShippingInformationsHistoryHandler;

    public MaterialShippingInformationsHistoryController(
        ICreateMaterialShippingInformationHistoryHandler createMaterialShippingInformationHistoryHandler,
        IReadMaterialShippingInformationHistoryHandler readMaterialShippingInformationHistoryHandler,
        IUpdateMaterialShippingInformationHistoryHandler updateMaterialShippingInformationHistoryHandler,
        IDeleteMaterialShippingInformationHistoryHandler deleteMaterialShippingInformationHistoryHandler,
        IListMaterialShippingInformationsHistoryHandler listMaterialShippingInformationsHistoryHandler)
    {
        _createMaterialShippingInformationHistoryHandler = createMaterialShippingInformationHistoryHandler;
        _readMaterialShippingInformationHistoryHandler = readMaterialShippingInformationHistoryHandler;
        _updateMaterialShippingInformationHistoryHandler = updateMaterialShippingInformationHistoryHandler;
        _deleteMaterialShippingInformationHistoryHandler = deleteMaterialShippingInformationHistoryHandler;
        _listMaterialShippingInformationsHistoryHandler = listMaterialShippingInformationsHistoryHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<CreateMaterialShippingInformationHistoryCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialShippingInformationHistoryCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateMaterialShippingInformationHistoryCommandResponse, Errors> result =
            await _createMaterialShippingInformationHistoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<DeleteMaterialShippingInformationHistoryCommandResponse, Errors> result =
            await _deleteMaterialShippingInformationHistoryHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialShippingInformationsHistoryQueryResponse, Errors> result =
            await _listMaterialShippingInformationsHistoryHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialShippingInformationHistoryQueryResponse, Errors> result =
            await _readMaterialShippingInformationHistoryHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<UpdateMaterialShippingInformationHistoryCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialShippingInformationHistoryCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateMaterialShippingInformationHistoryCommandResponse, Errors> result =
            await _updateMaterialShippingInformationHistoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
