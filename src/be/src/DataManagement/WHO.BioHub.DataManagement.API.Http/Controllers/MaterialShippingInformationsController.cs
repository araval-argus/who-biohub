using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.CreateMaterialShippingInformation;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.DeleteMaterialShippingInformation;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ListMaterialShippingInformations;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ReadMaterialShippingInformation;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.UpdateMaterialShippingInformation;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialShippingInformationsController
{
    Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class MaterialShippingInformationsController : ControllerBase, IMaterialShippingInformationsController
{
    private readonly ICreateMaterialShippingInformationHandler _createMaterialShippingInformationHandler;
    private readonly IReadMaterialShippingInformationHandler _readMaterialShippingInformationHandler;
    private readonly IUpdateMaterialShippingInformationHandler _updateMaterialShippingInformationHandler;
    private readonly IDeleteMaterialShippingInformationHandler _deleteMaterialShippingInformationHandler;
    private readonly IListMaterialShippingInformationsHandler _listMaterialShippingInformationsHandler;

    public MaterialShippingInformationsController(
        ICreateMaterialShippingInformationHandler createMaterialShippingInformationHandler,
        IReadMaterialShippingInformationHandler readMaterialShippingInformationHandler,
        IUpdateMaterialShippingInformationHandler updateMaterialShippingInformationHandler,
        IDeleteMaterialShippingInformationHandler deleteMaterialShippingInformationHandler,
        IListMaterialShippingInformationsHandler listMaterialShippingInformationsHandler)
    {
        _createMaterialShippingInformationHandler = createMaterialShippingInformationHandler;
        _readMaterialShippingInformationHandler = readMaterialShippingInformationHandler;
        _updateMaterialShippingInformationHandler = updateMaterialShippingInformationHandler;
        _deleteMaterialShippingInformationHandler = deleteMaterialShippingInformationHandler;
        _listMaterialShippingInformationsHandler = listMaterialShippingInformationsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<CreateMaterialShippingInformationCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialShippingInformationCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateMaterialShippingInformationCommandResponse, Errors> result =
            await _createMaterialShippingInformationHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<DeleteMaterialShippingInformationCommandResponse, Errors> result =
            await _deleteMaterialShippingInformationHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListMaterialShippingInformationsQueryResponse, Errors> result =
            await _listMaterialShippingInformationsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadMaterialShippingInformationQueryResponse, Errors> result =
            await _readMaterialShippingInformationHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<UpdateMaterialShippingInformationCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialShippingInformationCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateMaterialShippingInformationCommandResponse, Errors> result =
            await _updateMaterialShippingInformationHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
