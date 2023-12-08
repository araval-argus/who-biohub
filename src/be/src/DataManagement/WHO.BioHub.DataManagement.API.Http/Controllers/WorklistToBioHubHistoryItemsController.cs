using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.CreateWorklistToBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DeleteWorklistToBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ListWorklistToBioHubHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.UpdateWorklistToBioHubHistoryItem;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using System.Linq;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DownloadWorklistToBioHubHistoryItemFile;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IWorklistToBioHubHistoryItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
}

public class WorklistToBioHubHistoryItemsController : BaseIdentityController, IWorklistToBioHubHistoryItemsController
{
    private readonly ICreateWorklistToBioHubHistoryItemHandler _createWorklistToBioHubHistoryItemHandler;
    private readonly IReadWorklistToBioHubHistoryItemHandler _readWorklistToBioHubHistoryItemHandler;
    private readonly IUpdateWorklistToBioHubHistoryItemHandler _updateWorklistToBioHubHistoryItemHandler;
    private readonly IDeleteWorklistToBioHubHistoryItemHandler _deleteWorklistToBioHubHistoryItemHandler;
    private readonly IListWorklistToBioHubHistoryItemsHandler _listWorklistToBioHubHistoryItemsHandler;
    private readonly IDownloadWorklistToBioHubHistoryItemFileHandler _downloadWorklistToBioHubHistoryItemFileHandler;

    public WorklistToBioHubHistoryItemsController(
        ICreateWorklistToBioHubHistoryItemHandler createWorklistToBioHubHistoryItemHandler,
        IReadWorklistToBioHubHistoryItemHandler readWorklistToBioHubHistoryItemHandler,
        IUpdateWorklistToBioHubHistoryItemHandler updateWorklistToBioHubHistoryItemHandler,
        IDeleteWorklistToBioHubHistoryItemHandler deleteWorklistToBioHubHistoryItemHandler,
        IListWorklistToBioHubHistoryItemsHandler listWorklistToBioHubHistoryItemsHandler,
        IDownloadWorklistToBioHubHistoryItemFileHandler downloadWorklistToBioHubHistoryItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createWorklistToBioHubHistoryItemHandler = createWorklistToBioHubHistoryItemHandler;
        _readWorklistToBioHubHistoryItemHandler = readWorklistToBioHubHistoryItemHandler;
        _updateWorklistToBioHubHistoryItemHandler = updateWorklistToBioHubHistoryItemHandler;
        _deleteWorklistToBioHubHistoryItemHandler = deleteWorklistToBioHubHistoryItemHandler;
        _listWorklistToBioHubHistoryItemsHandler = listWorklistToBioHubHistoryItemsHandler;
        _downloadWorklistToBioHubHistoryItemFileHandler = downloadWorklistToBioHubHistoryItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CreateWorklistToBioHubHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<CreateWorklistToBioHubHistoryItemCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateWorklistToBioHubHistoryItemCommandResponse, Errors> result =
            await _createWorklistToBioHubHistoryItemHandler.Handle(body.Left, cancellationToken);
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

        Either<DeleteWorklistToBioHubHistoryItemCommandResponse, Errors> result =
            await _deleteWorklistToBioHubHistoryItemHandler.Handle(new(Id: id), cancellationToken);
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

        var query = new ListWorklistToBioHubHistoryItemsQuery();
        query.WorlistToBioHubItemId = worklistId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ListWorklistToBioHubHistoryItemsQueryResponse, Errors> result =
            await _listWorklistToBioHubHistoryItemsHandler.Handle(query, cancellationToken);

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

        var query = new ReadWorklistToBioHubHistoryItemQuery();
        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ReadWorklistToBioHubHistoryItemQueryResponse, Errors> result =
            await _readWorklistToBioHubHistoryItemHandler.Handle(query, cancellationToken);

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

        Either<UpdateWorklistToBioHubHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<UpdateWorklistToBioHubHistoryItemCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateWorklistToBioHubHistoryItemCommandResponse, Errors> result =
            await _updateWorklistToBioHubHistoryItemHandler.Handle(body.Left, cancellationToken);
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

        var query = new DownloadWorklistToBioHubHistoryItemFileQuery();

        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = userPermissions;
        query.WorklistId = worklistId;


        Either<DownloadWorklistToBioHubHistoryItemFileQueryResponse, Errors> result =
            await _downloadWorklistToBioHubHistoryItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
