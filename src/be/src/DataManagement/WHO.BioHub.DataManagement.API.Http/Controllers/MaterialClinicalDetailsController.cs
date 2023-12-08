using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.CreateMaterialClinicalDetail;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.DeleteMaterialClinicalDetail;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ListMaterialClinicalDetails;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ReadMaterialClinicalDetail;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.UpdateMaterialClinicalDetail;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialClinicalDetailsController
{
    Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialClinicalDetailsController : ControllerBase, IMaterialClinicalDetailsController
{
    private readonly ICreateMaterialClinicalDetailHandler _createMaterialClinicalDetailHandler;
    private readonly IReadMaterialClinicalDetailHandler _readMaterialClinicalDetailHandler;
    private readonly IUpdateMaterialClinicalDetailHandler _updateMaterialClinicalDetailHandler;
    private readonly IDeleteMaterialClinicalDetailHandler _deleteMaterialClinicalDetailHandler;
    private readonly IListMaterialClinicalDetailsHandler _listMaterialClinicalDetailsHandler;

    public MaterialClinicalDetailsController(
        ICreateMaterialClinicalDetailHandler createMaterialClinicalDetailHandler,
        IReadMaterialClinicalDetailHandler readMaterialClinicalDetailHandler,
        IUpdateMaterialClinicalDetailHandler updateMaterialClinicalDetailHandler,
        IDeleteMaterialClinicalDetailHandler deleteMaterialClinicalDetailHandler,
        IListMaterialClinicalDetailsHandler listMaterialClinicalDetailsHandler)
    {
        _createMaterialClinicalDetailHandler = createMaterialClinicalDetailHandler;
        _readMaterialClinicalDetailHandler = readMaterialClinicalDetailHandler;
        _updateMaterialClinicalDetailHandler = updateMaterialClinicalDetailHandler;
        _deleteMaterialClinicalDetailHandler = deleteMaterialClinicalDetailHandler;
        _listMaterialClinicalDetailsHandler = listMaterialClinicalDetailsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<CreateMaterialClinicalDetailCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialClinicalDetailCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateMaterialClinicalDetailCommandResponse, Errors> result =
            await _createMaterialClinicalDetailHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<DeleteMaterialClinicalDetailCommandResponse, Errors> result =
            await _deleteMaterialClinicalDetailHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialClinicalDetailsQueryResponse, Errors> result =
            await _listMaterialClinicalDetailsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialClinicalDetailQueryResponse, Errors> result =
            await _readMaterialClinicalDetailHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<UpdateMaterialClinicalDetailCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialClinicalDetailCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateMaterialClinicalDetailCommandResponse, Errors> result =
            await _updateMaterialClinicalDetailHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
