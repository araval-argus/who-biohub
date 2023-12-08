using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.DeleteIsolationTechniqueType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ReadIsolationTechniqueType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IIsolationTechniqueTypesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class IsolationTechniqueTypesController : BaseIdentityController, IIsolationTechniqueTypesController
{
    private readonly ICreateIsolationTechniqueTypeHandler _createIsolationTechniqueTypeHandler;
    private readonly IReadIsolationTechniqueTypeHandler _readIsolationTechniqueTypeHandler;
    private readonly IUpdateIsolationTechniqueTypeHandler _updateIsolationTechniqueTypeHandler;
    private readonly IDeleteIsolationTechniqueTypeHandler _deleteIsolationTechniqueTypeHandler;
    private readonly IListIsolationTechniqueTypesHandler _listIsolationTechniqueTypesHandler;

    public IsolationTechniqueTypesController(
        ICreateIsolationTechniqueTypeHandler createIsolationTechniqueTypeHandler,
        IReadIsolationTechniqueTypeHandler readIsolationTechniqueTypeHandler,
        IUpdateIsolationTechniqueTypeHandler updateIsolationTechniqueTypeHandler,
        IDeleteIsolationTechniqueTypeHandler deleteIsolationTechniqueTypeHandler,
        IListIsolationTechniqueTypesHandler listIsolationTechniqueTypesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createIsolationTechniqueTypeHandler = createIsolationTechniqueTypeHandler;
        _readIsolationTechniqueTypeHandler = readIsolationTechniqueTypeHandler;
        _updateIsolationTechniqueTypeHandler = updateIsolationTechniqueTypeHandler;
        _deleteIsolationTechniqueTypeHandler = deleteIsolationTechniqueTypeHandler;
        _listIsolationTechniqueTypesHandler = listIsolationTechniqueTypesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateIsolationTechniqueType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateIsolationTechniqueTypeCommand, Errors> body =
            await request.ParseBodyJson<CreateIsolationTechniqueTypeCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateIsolationTechniqueTypeCommandResponse, Errors> result =
            await _createIsolationTechniqueTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteIsolationTechniqueType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteIsolationTechniqueTypeCommandResponse, Errors> result =
            await _deleteIsolationTechniqueTypeHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadIsolationTechniqueType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListIsolationTechniqueTypesQueryResponse, Errors> result =
            await _listIsolationTechniqueTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadIsolationTechniqueType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadIsolationTechniqueTypeQueryResponse, Errors> result =
            await _readIsolationTechniqueTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditIsolationTechniqueType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateIsolationTechniqueTypeCommand, Errors> body =
            await request.ParseBodyJson<UpdateIsolationTechniqueTypeCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateIsolationTechniqueTypeCommandResponse, Errors> result =
            await _updateIsolationTechniqueTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
