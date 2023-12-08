using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DeleteSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;
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
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DownloadSMTA1WorkflowItemFile;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListDashboardSMTA1WorkflowItems;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface ISMTA1WorkflowItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> ListForDashboard(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class SMTA1WorkflowItemsController : BaseIdentityController, ISMTA1WorkflowItemsController
{
    private readonly ICreateSMTA1WorkflowItemHandler _createSMTA1WorkflowItemHandler;
    private readonly IReadSMTA1WorkflowItemHandler _readSMTA1WorkflowItemHandler;
    private readonly IUpdateSMTA1WorkflowItemHandler _updateSMTA1WorkflowItemHandler;
    private readonly IDeleteSMTA1WorkflowItemHandler _deleteSMTA1WorkflowItemHandler;
    private readonly IListSMTA1WorkflowItemsHandler _listSMTA1WorkflowItemsHandler;
    private readonly IListDashboardSMTA1WorkflowItemsHandler _listDashboardSMTA1WorkflowItemsHandler;
    private readonly ICreateUserFromUserRequestHandler _createUserFromUserRequestHandler;
    private readonly IAzureADUserInvitation _azureADUserInvitation;
    private readonly IDownloadSMTA1WorkflowItemFileHandler _downloadSMTA1WorkflowItemFileHandler;

    public SMTA1WorkflowItemsController(
        ICreateSMTA1WorkflowItemHandler createSMTA1WorkflowItemHandler,
        IReadSMTA1WorkflowItemHandler readSMTA1WorkflowItemHandler,
        IUpdateSMTA1WorkflowItemHandler updateSMTA1WorkflowItemHandler,
        IDeleteSMTA1WorkflowItemHandler deleteSMTA1WorkflowItemHandler,
        IListSMTA1WorkflowItemsHandler listSMTA1WorkflowItemsHandler,
        IListDashboardSMTA1WorkflowItemsHandler listDashboardSMTA1WorkflowItemsHandler,
        IDownloadSMTA1WorkflowItemFileHandler downloadSMTA1WorkflowItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createSMTA1WorkflowItemHandler = createSMTA1WorkflowItemHandler;
        _readSMTA1WorkflowItemHandler = readSMTA1WorkflowItemHandler;
        _updateSMTA1WorkflowItemHandler = updateSMTA1WorkflowItemHandler;
        _deleteSMTA1WorkflowItemHandler = deleteSMTA1WorkflowItemHandler;
        _listSMTA1WorkflowItemsHandler = listSMTA1WorkflowItemsHandler;
        _listDashboardSMTA1WorkflowItemsHandler = listDashboardSMTA1WorkflowItemsHandler;
        _downloadSMTA1WorkflowItemFileHandler = downloadSMTA1WorkflowItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateSMTA1WorkflowItemCommand, Errors> body =
            await request.ParseBodyJson<CreateSMTA1WorkflowItemCommand>(cancellationToken);
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

        Either<CreateSMTA1WorkflowItemCommandResponse, Errors> result =
            await _createSMTA1WorkflowItemHandler.Handle(command, cancellationToken);
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

        Either<DeleteSMTA1WorkflowItemCommandResponse, Errors> result =
            await _deleteSMTA1WorkflowItemHandler.Handle(new(Id: id), cancellationToken);
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

        var query = new ListSMTA1WorkflowItemsQuery();

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
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ListSMTA1WorkflowItemsQueryResponse, Errors> result =
            await _listSMTA1WorkflowItemsHandler.Handle(query, cancellationToken);

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

        var query = new ListDashboardSMTA1WorkflowItemsQuery();

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
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ListDashboardSMTA1WorkflowItemsQueryResponse, Errors> result =
            await _listDashboardSMTA1WorkflowItemsHandler.Handle(query, cancellationToken);

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

        var query = new ReadSMTA1WorkflowItemQuery();

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
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ReadSMTA1WorkflowItemQueryResponse, Errors> result =
            await _readSMTA1WorkflowItemHandler.Handle(query, cancellationToken);

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

        UpdateSMTA1WorkflowItemCommand command = JsonConvert.DeserializeObject<UpdateSMTA1WorkflowItemCommand>(request.Form["Command"][0]);

        command.UserId = userLoginInfo.UserId;

        IFormFile? file = request.Form.Files.Any() ? request.Form.Files.FirstOrDefault() : null;
        command.File = file;
        command.UserPermissions = userPermissions;
        command.RoleType = userLoginInfo?.RoleType;
        command.UserLaboratoryId = userLoginInfo?.LaboratoryId;

        Either<UpdateSMTA1WorkflowItemCommandResponse, Errors> result =
            await _updateSMTA1WorkflowItemHandler.Handle(command, cancellationToken);
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

        var query = new DownloadSMTA1WorkflowItemFileQuery();

        query.Id = id;
        query.UserPermissions = userPermissions;
        query.WorkflowId = worklistId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;


        Either<DownloadSMTA1WorkflowItemFileQueryResponse, Errors> result =
            await _downloadSMTA1WorkflowItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
