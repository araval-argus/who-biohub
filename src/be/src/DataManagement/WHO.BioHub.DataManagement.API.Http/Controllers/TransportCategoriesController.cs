using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.CreateTransportCategory;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.DeleteTransportCategory;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ListTransportCategories;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ReadTransportCategory;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.UpdateTransportCategory;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ITransportCategoriesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class TransportCategoriesController : BaseIdentityController, ITransportCategoriesController
{
    private readonly ICreateTransportCategoryHandler _createTransportCategoryHandler;
    private readonly IReadTransportCategoryHandler _readTransportCategoryHandler;
    private readonly IUpdateTransportCategoryHandler _updateTransportCategoryHandler;
    private readonly IDeleteTransportCategoryHandler _deleteTransportCategoryHandler;
    private readonly IListTransportCategoriesHandler _listTransportCategoriesHandler;

    public TransportCategoriesController(
        ICreateTransportCategoryHandler createTransportCategoryHandler,
        IReadTransportCategoryHandler readTransportCategoryHandler,
        IUpdateTransportCategoryHandler updateTransportCategoryHandler,
        IDeleteTransportCategoryHandler deleteTransportCategoryHandler,
        IListTransportCategoriesHandler listTransportCategoriesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createTransportCategoryHandler = createTransportCategoryHandler;
        _readTransportCategoryHandler = readTransportCategoryHandler;
        _updateTransportCategoryHandler = updateTransportCategoryHandler;
        _deleteTransportCategoryHandler = deleteTransportCategoryHandler;
        _listTransportCategoriesHandler = listTransportCategoriesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateTransportCategory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateTransportCategoryCommand, Errors> body =
            await request.ParseBodyJson<CreateTransportCategoryCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateTransportCategoryCommandResponse, Errors> result =
            await _createTransportCategoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteTransportCategory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteTransportCategoryCommandResponse, Errors> result =
            await _deleteTransportCategoryHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadTransportCategory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListTransportCategoriesQueryResponse, Errors> result =
            await _listTransportCategoriesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadTransportCategory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadTransportCategoryQueryResponse, Errors> result =
            await _readTransportCategoryHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditTransportCategory);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateTransportCategoryCommand, Errors> body =
            await request.ParseBodyJson<UpdateTransportCategoryCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateTransportCategoryCommandResponse, Errors> result =
            await _updateTransportCategoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
