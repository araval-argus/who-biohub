using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.CreateSpecimenType;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.DeleteSpecimenType;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ListSpecimenTypes;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ReadSpecimenType;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.UpdateSpecimenType;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISpecimenTypesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class SpecimenTypesController : BaseIdentityController, ISpecimenTypesController
{
    private readonly ICreateSpecimenTypeHandler _createSpecimenTypeHandler;
    private readonly IReadSpecimenTypeHandler _readSpecimenTypeHandler;
    private readonly IUpdateSpecimenTypeHandler _updateSpecimenTypeHandler;
    private readonly IDeleteSpecimenTypeHandler _deleteSpecimenTypeHandler;
    private readonly IListSpecimenTypesHandler _listSpecimenTypesHandler;

    public SpecimenTypesController(
        ICreateSpecimenTypeHandler createSpecimenTypeHandler,
        IReadSpecimenTypeHandler readSpecimenTypeHandler,
        IUpdateSpecimenTypeHandler updateSpecimenTypeHandler,
        IDeleteSpecimenTypeHandler deleteSpecimenTypeHandler,
        IListSpecimenTypesHandler listSpecimenTypesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createSpecimenTypeHandler = createSpecimenTypeHandler;
        _readSpecimenTypeHandler = readSpecimenTypeHandler;
        _updateSpecimenTypeHandler = updateSpecimenTypeHandler;
        _deleteSpecimenTypeHandler = deleteSpecimenTypeHandler;
        _listSpecimenTypesHandler = listSpecimenTypesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateSpecimenType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateSpecimenTypeCommand, Errors> body =
            await request.ParseBodyJson<CreateSpecimenTypeCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateSpecimenTypeCommandResponse, Errors> result =
            await _createSpecimenTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteSpecimenType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteSpecimenTypeCommandResponse, Errors> result =
            await _deleteSpecimenTypeHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadSpecimenType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListSpecimenTypesQueryResponse, Errors> result =
            await _listSpecimenTypesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {

        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadSpecimenType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadSpecimenTypeQueryResponse, Errors> result =
            await _readSpecimenTypeHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditSpecimenType);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateSpecimenTypeCommand, Errors> body =
            await request.ParseBodyJson<UpdateSpecimenTypeCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateSpecimenTypeCommandResponse, Errors> result =
            await _updateSpecimenTypeHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
