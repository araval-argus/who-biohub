using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.DeleteTemperatureUnitOfMeasure;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ReadTemperatureUnitOfMeasure;
using WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ITemperatureUnitOfMeasuresController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class TemperatureUnitOfMeasuresController : BaseIdentityController, ITemperatureUnitOfMeasuresController
{
    private readonly ICreateTemperatureUnitOfMeasureHandler _createTemperatureUnitOfMeasureHandler;
    private readonly IReadTemperatureUnitOfMeasureHandler _readTemperatureUnitOfMeasureHandler;
    private readonly IUpdateTemperatureUnitOfMeasureHandler _updateTemperatureUnitOfMeasureHandler;
    private readonly IDeleteTemperatureUnitOfMeasureHandler _deleteTemperatureUnitOfMeasureHandler;
    private readonly IListTemperatureUnitOfMeasuresHandler _listTemperatureUnitOfMeasuresHandler;

    public TemperatureUnitOfMeasuresController(
        ICreateTemperatureUnitOfMeasureHandler createTemperatureUnitOfMeasureHandler,
        IReadTemperatureUnitOfMeasureHandler readTemperatureUnitOfMeasureHandler,
        IUpdateTemperatureUnitOfMeasureHandler updateTemperatureUnitOfMeasureHandler,
        IDeleteTemperatureUnitOfMeasureHandler deleteTemperatureUnitOfMeasureHandler,
        IListTemperatureUnitOfMeasuresHandler listTemperatureUnitOfMeasuresHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createTemperatureUnitOfMeasureHandler = createTemperatureUnitOfMeasureHandler;
        _readTemperatureUnitOfMeasureHandler = readTemperatureUnitOfMeasureHandler;
        _updateTemperatureUnitOfMeasureHandler = updateTemperatureUnitOfMeasureHandler;
        _deleteTemperatureUnitOfMeasureHandler = deleteTemperatureUnitOfMeasureHandler;
        _listTemperatureUnitOfMeasuresHandler = listTemperatureUnitOfMeasuresHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateTemperatureUnitOfMeasure);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateTemperatureUnitOfMeasureCommand, Errors> body =
            await request.ParseBodyJson<CreateTemperatureUnitOfMeasureCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateTemperatureUnitOfMeasureCommandResponse, Errors> result =
            await _createTemperatureUnitOfMeasureHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteTemperatureUnitOfMeasure);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteTemperatureUnitOfMeasureCommandResponse, Errors> result =
            await _deleteTemperatureUnitOfMeasureHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadTemperatureUnitOfMeasure);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListTemperatureUnitOfMeasuresQueryResponse, Errors> result =
            await _listTemperatureUnitOfMeasuresHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadTemperatureUnitOfMeasure);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadTemperatureUnitOfMeasureQueryResponse, Errors> result =
            await _readTemperatureUnitOfMeasureHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditTemperatureUnitOfMeasure);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateTemperatureUnitOfMeasureCommand, Errors> body =
            await request.ParseBodyJson<UpdateTemperatureUnitOfMeasureCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateTemperatureUnitOfMeasureCommandResponse, Errors> result =
            await _updateTemperatureUnitOfMeasureHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
