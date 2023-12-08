using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateResource;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResource;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.ListResources;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.ReadResourceFileToken;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.UpdateResource;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.UploadResourceFileToken;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResourceFileToken;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Identity;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateFolder;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IResourcesController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ReadFileToken(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> UploadFileToken(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> DeleteFileToken(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> CreateFolder(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class ResourcesController : BaseIdentityController, IResourcesController
{
    private readonly ICreateResourceHandler _createResourceHandler;
    private readonly ICreateFolderHandler _createFolderHandler;
    private readonly IReadResourceFileTokenHandler _readResourceFileTokenHandler;
    private readonly IUploadResourceFileTokenHandler _uploadResourceFileTokenHandler;
    private readonly IDeleteResourceFileTokenHandler _deleteResourceFileTokenHandler;
    private readonly IUpdateResourceHandler _updateResourceHandler;
    private readonly IDeleteResourceHandler _deleteResourceHandler;
    private readonly IListResourcesHandler _listResourcesHandler;

    public ResourcesController(
        ICreateResourceHandler createResourceHandler,
        ICreateFolderHandler createFolderHandler,
        IReadResourceFileTokenHandler readResourceHandler,
        IUploadResourceFileTokenHandler uploadResourceFileTokenHandler,
        IDeleteResourceFileTokenHandler deleteResourceFileTokenHandler,
        IUpdateResourceHandler updateResourceHandler,
        IDeleteResourceHandler deleteResourceHandler,
        IListResourcesHandler listResourcesHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createResourceHandler = createResourceHandler;
        _createFolderHandler = createFolderHandler;
        _readResourceFileTokenHandler = readResourceHandler;
        _uploadResourceFileTokenHandler = uploadResourceFileTokenHandler;
        _deleteResourceFileTokenHandler = deleteResourceFileTokenHandler;
        _updateResourceHandler = updateResourceHandler;
        _deleteResourceHandler = deleteResourceHandler;
        _listResourcesHandler = listResourcesHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateResourceCommand, Errors> body =
            await request.ParseBodyJson<CreateResourceCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var command = body.Left;
        var userLoginInfo = checkUserPermissionResult.Left;

        command.UploadedById = userLoginInfo.UserId;

        Either<CreateResourceCommandResponse, Errors> result =
            await _createResourceHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> CreateFolder(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateFolderCommand, Errors> body =
            await request.ParseBodyJson<CreateFolderCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateFolderCommandResponse, Errors> result =
            await _createFolderHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteResourceCommandResponse, Errors> result =
            await _deleteResourceHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListResourcesQueryResponse, Errors> result =
            await _listResourcesHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadFileToken(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadResourceFileTokenQueryResponse, Errors> result =
            await _readResourceFileTokenHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> UploadFileToken(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UploadResourceFileTokenQuery, Errors> body =
            await request.ParseBodyJson<UploadResourceFileTokenQuery>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UploadResourceFileTokenQueryResponse, Errors> result =
            await _uploadResourceFileTokenHandler.Handle(body.Left, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> DeleteFileToken(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteResourceFileTokenQueryResponse, Errors> result =
            await _deleteResourceFileTokenHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditResource);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateResourceCommand, Errors> body =
            await request.ParseBodyJson<UpdateResourceCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateResourceCommandResponse, Errors> result =
            await _updateResourceHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
