using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.CreateSMTA2WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DeleteSMTA2WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ListSMTA2WorkflowHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.UpdateSMTA2WorkflowHistoryItem;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using System.Linq;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DownloadSMTA2WorkflowHistoryItemFile;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISMTA2WorkflowHistoryItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
}

public class SMTA2WorkflowHistoryItemsController : BaseIdentityController, ISMTA2WorkflowHistoryItemsController
{
    private readonly ICreateSMTA2WorkflowHistoryItemHandler _createSMTA2WorkflowHistoryItemHandler;
    private readonly IReadSMTA2WorkflowHistoryItemHandler _readSMTA2WorkflowHistoryItemHandler;
    private readonly IUpdateSMTA2WorkflowHistoryItemHandler _updateSMTA2WorkflowHistoryItemHandler;
    private readonly IDeleteSMTA2WorkflowHistoryItemHandler _deleteSMTA2WorkflowHistoryItemHandler;
    private readonly IListSMTA2WorkflowHistoryItemsHandler _listSMTA2WorkflowHistoryItemsHandler;
    private readonly IDownloadSMTA2WorkflowHistoryItemFileHandler _downloadSMTA2WorkflowHistoryItemFileHandler;

    public SMTA2WorkflowHistoryItemsController(
        ICreateSMTA2WorkflowHistoryItemHandler createSMTA2WorkflowHistoryItemHandler,
        IReadSMTA2WorkflowHistoryItemHandler readSMTA2WorkflowHistoryItemHandler,
        IUpdateSMTA2WorkflowHistoryItemHandler updateSMTA2WorkflowHistoryItemHandler,
        IDeleteSMTA2WorkflowHistoryItemHandler deleteSMTA2WorkflowHistoryItemHandler,
        IListSMTA2WorkflowHistoryItemsHandler listSMTA2WorkflowHistoryItemsHandler,
        IDownloadSMTA2WorkflowHistoryItemFileHandler downloadSMTA2WorkflowHistoryItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createSMTA2WorkflowHistoryItemHandler = createSMTA2WorkflowHistoryItemHandler;
        _readSMTA2WorkflowHistoryItemHandler = readSMTA2WorkflowHistoryItemHandler;
        _updateSMTA2WorkflowHistoryItemHandler = updateSMTA2WorkflowHistoryItemHandler;
        _deleteSMTA2WorkflowHistoryItemHandler = deleteSMTA2WorkflowHistoryItemHandler;
        _listSMTA2WorkflowHistoryItemsHandler = listSMTA2WorkflowHistoryItemsHandler;
        _downloadSMTA2WorkflowHistoryItemFileHandler = downloadSMTA2WorkflowHistoryItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CreateSMTA2WorkflowHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<CreateSMTA2WorkflowHistoryItemCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateSMTA2WorkflowHistoryItemCommandResponse, Errors> result =
            await _createSMTA2WorkflowHistoryItemHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteSMTA2WorkflowHistoryItemCommandResponse, Errors> result =
            await _deleteSMTA2WorkflowHistoryItemHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessWorklist);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var query = new ListSMTA2WorkflowHistoryItemsQuery();
        query.WorlistToBioHubItemId = worklistId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ListSMTA2WorkflowHistoryItemsQueryResponse, Errors> result =
            await _listSMTA2WorkflowHistoryItemsHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessWorklist);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var query = new ReadSMTA2WorkflowHistoryItemQuery();
        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ReadSMTA2WorkflowHistoryItemQueryResponse, Errors> result =
            await _readSMTA2WorkflowHistoryItemHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    private IEnumerable<string> GetUserPermissions(Either<UserLoginInfo, Errors> checkUserPermissionResult)
    {
        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);
        if (userPermissions != null)
        {
            return userPermissions;
        }

        return new List<string>();

    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateSMTA2WorkflowHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<UpdateSMTA2WorkflowHistoryItemCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateSMTA2WorkflowHistoryItemCommandResponse, Errors> result =
            await _updateSMTA2WorkflowHistoryItemHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessWorklist);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        var query = new DownloadSMTA2WorkflowHistoryItemFileQuery();

        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserPermissions = userPermissions;
        query.WorklistId = worklistId;


        Either<DownloadSMTA2WorkflowHistoryItemFileQueryResponse, Errors> result =
            await _downloadSMTA2WorkflowHistoryItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
