using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.CreateWorklistFromBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DeleteWorklistFromBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ListWorklistFromBioHubHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ReadWorklistFromBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.UpdateWorklistFromBioHubHistoryItem;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using System.Linq;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DownloadWorklistFromBioHubHistoryItemFile;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IWorklistFromBioHubHistoryItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
}

public class WorklistFromBioHubHistoryItemsController : BaseIdentityController, IWorklistFromBioHubHistoryItemsController
{
    private readonly ICreateWorklistFromBioHubHistoryItemHandler _createWorklistFromBioHubHistoryItemHandler;
    private readonly IReadWorklistFromBioHubHistoryItemHandler _readWorklistFromBioHubHistoryItemHandler;
    private readonly IUpdateWorklistFromBioHubHistoryItemHandler _updateWorklistFromBioHubHistoryItemHandler;
    private readonly IDeleteWorklistFromBioHubHistoryItemHandler _deleteWorklistFromBioHubHistoryItemHandler;
    private readonly IListWorklistFromBioHubHistoryItemsHandler _listWorklistFromBioHubHistoryItemsHandler;
    private readonly IDownloadWorklistFromBioHubHistoryItemFileHandler _downloadWorklistFromBioHubHistoryItemFileHandler;

    public WorklistFromBioHubHistoryItemsController(
        ICreateWorklistFromBioHubHistoryItemHandler createWorklistFromBioHubHistoryItemHandler,
        IReadWorklistFromBioHubHistoryItemHandler readWorklistFromBioHubHistoryItemHandler,
        IUpdateWorklistFromBioHubHistoryItemHandler updateWorklistFromBioHubHistoryItemHandler,
        IDeleteWorklistFromBioHubHistoryItemHandler deleteWorklistFromBioHubHistoryItemHandler,
        IListWorklistFromBioHubHistoryItemsHandler listWorklistFromBioHubHistoryItemsHandler,
        IDownloadWorklistFromBioHubHistoryItemFileHandler downloadWorklistFromBioHubHistoryItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createWorklistFromBioHubHistoryItemHandler = createWorklistFromBioHubHistoryItemHandler;
        _readWorklistFromBioHubHistoryItemHandler = readWorklistFromBioHubHistoryItemHandler;
        _updateWorklistFromBioHubHistoryItemHandler = updateWorklistFromBioHubHistoryItemHandler;
        _deleteWorklistFromBioHubHistoryItemHandler = deleteWorklistFromBioHubHistoryItemHandler;
        _listWorklistFromBioHubHistoryItemsHandler = listWorklistFromBioHubHistoryItemsHandler;
        _downloadWorklistFromBioHubHistoryItemFileHandler = downloadWorklistFromBioHubHistoryItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CreateWorklistFromBioHubHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<CreateWorklistFromBioHubHistoryItemCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateWorklistFromBioHubHistoryItemCommandResponse, Errors> result =
            await _createWorklistFromBioHubHistoryItemHandler.Handle(body.Left, cancellationToken);
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

        Either<DeleteWorklistFromBioHubHistoryItemCommandResponse, Errors> result =
            await _deleteWorklistFromBioHubHistoryItemHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }


        var query = new ListWorklistFromBioHubHistoryItemsQuery();
        query.WorlistFromBioHubItemId = worklistId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ListWorklistFromBioHubHistoryItemsQueryResponse, Errors> result =
            await _listWorklistFromBioHubHistoryItemsHandler.Handle(query, cancellationToken);

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

        var query = new ReadWorklistFromBioHubHistoryItemQuery();
        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ReadWorklistFromBioHubHistoryItemQueryResponse, Errors> result =
            await _readWorklistFromBioHubHistoryItemHandler.Handle(query, cancellationToken);

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

        Either<UpdateWorklistFromBioHubHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<UpdateWorklistFromBioHubHistoryItemCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateWorklistFromBioHubHistoryItemCommandResponse, Errors> result =
            await _updateWorklistFromBioHubHistoryItemHandler.Handle(body.Left, cancellationToken);
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

        var query = new DownloadWorklistFromBioHubHistoryItemFileQuery();

        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = userPermissions;
        query.WorklistId = worklistId;


        Either<DownloadWorklistFromBioHubHistoryItemFileQueryResponse, Errors> result =
            await _downloadWorklistFromBioHubHistoryItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
