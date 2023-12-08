using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.CreateSMTA1WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DeleteSMTA1WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ListSMTA1WorkflowHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.UpdateSMTA1WorkflowHistoryItem;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using System.Linq;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DownloadSMTA1WorkflowHistoryItemFile;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISMTA1WorkflowHistoryItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
}

public class SMTA1WorkflowHistoryItemsController : BaseIdentityController, ISMTA1WorkflowHistoryItemsController
{
    private readonly ICreateSMTA1WorkflowHistoryItemHandler _createSMTA1WorkflowHistoryItemHandler;
    private readonly IReadSMTA1WorkflowHistoryItemHandler _readSMTA1WorkflowHistoryItemHandler;
    private readonly IUpdateSMTA1WorkflowHistoryItemHandler _updateSMTA1WorkflowHistoryItemHandler;
    private readonly IDeleteSMTA1WorkflowHistoryItemHandler _deleteSMTA1WorkflowHistoryItemHandler;
    private readonly IListSMTA1WorkflowHistoryItemsHandler _listSMTA1WorkflowHistoryItemsHandler;
    private readonly IDownloadSMTA1WorkflowHistoryItemFileHandler _downloadSMTA1WorkflowHistoryItemFileHandler;

    public SMTA1WorkflowHistoryItemsController(
        ICreateSMTA1WorkflowHistoryItemHandler createSMTA1WorkflowHistoryItemHandler,
        IReadSMTA1WorkflowHistoryItemHandler readSMTA1WorkflowHistoryItemHandler,
        IUpdateSMTA1WorkflowHistoryItemHandler updateSMTA1WorkflowHistoryItemHandler,
        IDeleteSMTA1WorkflowHistoryItemHandler deleteSMTA1WorkflowHistoryItemHandler,
        IListSMTA1WorkflowHistoryItemsHandler listSMTA1WorkflowHistoryItemsHandler,
        IDownloadSMTA1WorkflowHistoryItemFileHandler downloadSMTA1WorkflowHistoryItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createSMTA1WorkflowHistoryItemHandler = createSMTA1WorkflowHistoryItemHandler;
        _readSMTA1WorkflowHistoryItemHandler = readSMTA1WorkflowHistoryItemHandler;
        _updateSMTA1WorkflowHistoryItemHandler = updateSMTA1WorkflowHistoryItemHandler;
        _deleteSMTA1WorkflowHistoryItemHandler = deleteSMTA1WorkflowHistoryItemHandler;
        _listSMTA1WorkflowHistoryItemsHandler = listSMTA1WorkflowHistoryItemsHandler;
        _downloadSMTA1WorkflowHistoryItemFileHandler = downloadSMTA1WorkflowHistoryItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CreateSMTA1WorkflowHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<CreateSMTA1WorkflowHistoryItemCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateSMTA1WorkflowHistoryItemCommandResponse, Errors> result =
            await _createSMTA1WorkflowHistoryItemHandler.Handle(body.Left, cancellationToken);
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

        Either<DeleteSMTA1WorkflowHistoryItemCommandResponse, Errors> result =
            await _deleteSMTA1WorkflowHistoryItemHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid worklistId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var query = new ListSMTA1WorkflowHistoryItemsQuery();
        query.WorlistToBioHubItemId = worklistId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ListSMTA1WorkflowHistoryItemsQueryResponse, Errors> result =
            await _listSMTA1WorkflowHistoryItemsHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var query = new ReadSMTA1WorkflowHistoryItemQuery();
        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = GetUserPermissions(checkUserPermissionResult);

        Either<ReadSMTA1WorkflowHistoryItemQueryResponse, Errors> result =
            await _readSMTA1WorkflowHistoryItemHandler.Handle(query, cancellationToken);

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

        Either<UpdateSMTA1WorkflowHistoryItemCommand, Errors> body =
            await request.ParseBodyJson<UpdateSMTA1WorkflowHistoryItemCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateSMTA1WorkflowHistoryItemCommandResponse, Errors> result =
            await _updateSMTA1WorkflowHistoryItemHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        var query = new DownloadSMTA1WorkflowHistoryItemFileQuery();

        query.Id = id;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.UserPermissions = userPermissions;
        query.WorklistId = worklistId;


        Either<DownloadSMTA1WorkflowHistoryItemFileQueryResponse, Errors> result =
            await _downloadSMTA1WorkflowHistoryItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
