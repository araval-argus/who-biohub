using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.CreateSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DeleteSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUserFromUserRequest;
using WHO.BioHub.Graph;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using System.Linq;
using Newtonsoft.Json;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DownloadSMTA2WorkflowItemFile;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItems;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISMTA2WorkflowItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> ListForDashboard(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class SMTA2WorkflowItemsController : BaseIdentityController, ISMTA2WorkflowItemsController
{
    private readonly ICreateSMTA2WorkflowItemHandler _createSMTA2WorkflowItemHandler;
    private readonly IReadSMTA2WorkflowItemHandler _readSMTA2WorkflowItemHandler;
    private readonly IUpdateSMTA2WorkflowItemHandler _updateSMTA2WorkflowItemHandler;
    private readonly IDeleteSMTA2WorkflowItemHandler _deleteSMTA2WorkflowItemHandler;
    private readonly IListSMTA2WorkflowItemsHandler _listSMTA2WorkflowItemsHandler;
    private readonly IListDashboardSMTA2WorkflowItemsHandler _listDashboardSMTA2WorkflowItemsHandler;
    private readonly ICreateUserFromUserRequestHandler _createUserFromUserRequestHandler;
    private readonly IAzureADUserInvitation _azureADUserInvitation;
    private readonly IDownloadSMTA2WorkflowItemFileHandler _downloadSMTA2WorkflowItemFileHandler;

    public SMTA2WorkflowItemsController(
        ICreateSMTA2WorkflowItemHandler createSMTA2WorkflowItemHandler,
        IReadSMTA2WorkflowItemHandler readSMTA2WorkflowItemHandler,
        IUpdateSMTA2WorkflowItemHandler updateSMTA2WorkflowItemHandler,
        IDeleteSMTA2WorkflowItemHandler deleteSMTA2WorkflowItemHandler,
        IListSMTA2WorkflowItemsHandler listSMTA2WorkflowItemsHandler,
        IListDashboardSMTA2WorkflowItemsHandler listDashboardSMTA2WorkflowItemsHandler,
        IDownloadSMTA2WorkflowItemFileHandler downloadSMTA2WorkflowItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createSMTA2WorkflowItemHandler = createSMTA2WorkflowItemHandler;
        _readSMTA2WorkflowItemHandler = readSMTA2WorkflowItemHandler;
        _updateSMTA2WorkflowItemHandler = updateSMTA2WorkflowItemHandler;
        _deleteSMTA2WorkflowItemHandler = deleteSMTA2WorkflowItemHandler;
        _listSMTA2WorkflowItemsHandler = listSMTA2WorkflowItemsHandler;
        _listDashboardSMTA2WorkflowItemsHandler = listDashboardSMTA2WorkflowItemsHandler;
        _downloadSMTA2WorkflowItemFileHandler = downloadSMTA2WorkflowItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateSMTA2WorkflowItemCommand, Errors> body =
            await request.ParseBodyJson<CreateSMTA2WorkflowItemCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        if (body.Left.IsPast == true)
        {

            if (!userLoginInfo.UserPermissions.Select(x => x.PermissionName).Contains(PermissionNames.CanAccessPastRequestIniziation))
            {
                return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
            }
        }
        else
        {
            if (!userLoginInfo.UserPermissions.Select(x => x.PermissionName).Contains(PermissionNames.CanAccessRequestIniziation))
            {
                return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
            }
        }

        var command = body.Left;

        command.LaboratoryId = userLoginInfo.LaboratoryId;
        command.UserId = userLoginInfo.UserId;

        Either<CreateSMTA2WorkflowItemCommandResponse, Errors> result =
            await _createSMTA2WorkflowItemHandler.Handle(command, cancellationToken);
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

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessSMTAWorkflow) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastSMTAWorkflow))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<DeleteSMTA2WorkflowItemCommandResponse, Errors> result =
            await _deleteSMTA2WorkflowItemHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var query = new ListSMTA2WorkflowItemsQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessSMTAWorkflow) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastSMTAWorkflow))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        query.UserPermissions = userPermissions;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;


        Either<ListSMTA2WorkflowItemsQueryResponse, Errors> result =
            await _listSMTA2WorkflowItemsHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> ListForDashboard(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var query = new ListDashboardSMTA2WorkflowItemsQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessSMTAWorkflow) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastSMTAWorkflow))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        query.UserPermissions = userPermissions;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;


        Either<ListDashboardSMTA2WorkflowItemsQueryResponse, Errors> result =
            await _listDashboardSMTA2WorkflowItemsHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var query = new ReadSMTA2WorkflowItemQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessSMTAWorkflow) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastSMTAWorkflow))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        query.Id = id;
        query.UserPermissions = userPermissions;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;


        Either<ReadSMTA2WorkflowItemQueryResponse, Errors> result =
            await _readSMTA2WorkflowItemHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessSMTAWorkflow) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastSMTAWorkflow))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        UpdateSMTA2WorkflowItemCommand command = JsonConvert.DeserializeObject<UpdateSMTA2WorkflowItemCommand>(request.Form["Command"][0]);

        command.UserId = userLoginInfo.UserId;

        IFormFile? file = request.Form.Files.Any() ? request.Form.Files.FirstOrDefault() : null;
        command.File = file;
        command.UserPermissions = userPermissions;
        command.RoleType = userLoginInfo?.RoleType;
        command.UserLaboratoryId = userLoginInfo?.LaboratoryId;


        Either<UpdateSMTA2WorkflowItemCommandResponse, Errors> result =
            await _updateSMTA2WorkflowItemHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessSMTAWorkflow) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastSMTAWorkflow))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        var query = new DownloadSMTA2WorkflowItemFileQuery();

        query.Id = id;
        query.UserPermissions = userPermissions;
        query.WorkflowId = worklistId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;


        Either<DownloadSMTA2WorkflowItemFileQueryResponse, Errors> result =
            await _downloadSMTA2WorkflowItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
