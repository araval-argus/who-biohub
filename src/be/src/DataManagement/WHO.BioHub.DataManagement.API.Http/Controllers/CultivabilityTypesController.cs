using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.CreateCultivabilityType;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.DeleteCultivabilityType;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ReadCultivabilityType;
using WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.UpdateCultivabilityType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ICultivabilityTypesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class CultivabilityTypesController : BaseIdentityController, ICultivabilityTypesController
{
    private readonly ICreateCultivabilityTypeHandler _createCultivabilityTypeHandler;
    private readonly IReadCultivabilityTypeHandler _readCultivabilityTypeHandler;
    private readonly IUpdateCultivabilityTypeHandler _updateCultivabilityTypeHandler;
    private readonly IDeleteCultivabilityTypeHandler _deleteCultivabilityTypeHandler;
    private readonly IListCultivabilityTypesHandler _listCultivabilityTypesHandler;

    public CultivabilityTypesController(
        ICreateCultivabilityTypeHandler createCultivabilityTypeHandler,
        IReadCultivabilityTypeHandler readCultivabilityTypeHandler,
        IUpdateCultivabilityTypeHandler updateCultivabilityTypeHandler,
        IDeleteCultivabilityTypeHandler deleteCultivabilityTypeHandler,
        IListCultivabilityTypesHandler listCultivabilityTypesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createCultivabilityTypeHandler = createCultivabilityTypeHandler;
        _readCultivabilityTypeHandler = readCultivabilityTypeHandler;
        _updateCultivabilityTypeHandler = updateCultivabilityTypeHandler;
        _deleteCultivabilityTypeHandler = deleteCultivabilityTypeHandler;
        _listCultivabilityTypesHandler = listCultivabilityTypesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateCultivabilityType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateCultivabilityTypeCommand, Errors> body =
            await request.ParseBodyJson<CreateCultivabilityTypeCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateCultivabilityTypeCommandResponse, Errors> result =
            await _createCultivabilityTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteCultivabilityType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteCultivabilityTypeCommandResponse, Errors> result =
            await _deleteCultivabilityTypeHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCultivabilityType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListCultivabilityTypesQueryResponse, Errors> result =
            await _listCultivabilityTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCultivabilityType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadCultivabilityTypeQueryResponse, Errors> result =
            await _readCultivabilityTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditCultivabilityType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateCultivabilityTypeCommand, Errors> body =
            await request.ParseBodyJson<UpdateCultivabilityTypeCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateCultivabilityTypeCommandResponse, Errors> result =
            await _updateCultivabilityTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
