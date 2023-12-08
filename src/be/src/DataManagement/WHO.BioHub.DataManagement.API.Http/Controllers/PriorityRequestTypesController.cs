using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.CreatePriorityRequestType;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.DeletePriorityRequestType;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ReadPriorityRequestType;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IPriorityRequestTypesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class PriorityRequestTypesController : BaseIdentityController, IPriorityRequestTypesController
{
    private readonly ICreatePriorityRequestTypeHandler _createPriorityRequestTypeHandler;
    private readonly IReadPriorityRequestTypeHandler _readPriorityRequestTypeHandler;
    private readonly IUpdatePriorityRequestTypeHandler _updatePriorityRequestTypeHandler;
    private readonly IDeletePriorityRequestTypeHandler _deletePriorityRequestTypeHandler;
    private readonly IListPriorityRequestTypesHandler _listPriorityRequestTypesHandler;

    public PriorityRequestTypesController(
        ICreatePriorityRequestTypeHandler createPriorityRequestTypeHandler,
        IReadPriorityRequestTypeHandler readPriorityRequestTypeHandler,
        IUpdatePriorityRequestTypeHandler updatePriorityRequestTypeHandler,
        IDeletePriorityRequestTypeHandler deletePriorityRequestTypeHandler,
        IListPriorityRequestTypesHandler listPriorityRequestTypesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createPriorityRequestTypeHandler = createPriorityRequestTypeHandler;
        _readPriorityRequestTypeHandler = readPriorityRequestTypeHandler;
        _updatePriorityRequestTypeHandler = updatePriorityRequestTypeHandler;
        _deletePriorityRequestTypeHandler = deletePriorityRequestTypeHandler;
        _listPriorityRequestTypesHandler = listPriorityRequestTypesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreatePriorityRequestType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreatePriorityRequestTypeCommand, Errors> body =
            await request.ParseBodyJson<CreatePriorityRequestTypeCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreatePriorityRequestTypeCommandResponse, Errors> result =
            await _createPriorityRequestTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeletePriorityRequestType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeletePriorityRequestTypeCommandResponse, Errors> result =
            await _deletePriorityRequestTypeHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadPriorityRequestType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListPriorityRequestTypesQueryResponse, Errors> result =
            await _listPriorityRequestTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadPriorityRequestType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadPriorityRequestTypeQueryResponse, Errors> result =
            await _readPriorityRequestTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditPriorityRequestType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdatePriorityRequestTypeCommand, Errors> body =
            await request.ParseBodyJson<UpdatePriorityRequestTypeCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdatePriorityRequestTypeCommandResponse, Errors> result =
            await _updatePriorityRequestTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
