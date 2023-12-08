using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DeleteWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;
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
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DownloadWorklistToBioHubItemFile;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItemShipmentDocuments;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IWorklistToBioHubItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> ListForDashboard(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> UpdateShipmentDocuments(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
}

public class WorklistToBioHubItemsController : BaseIdentityController, IWorklistToBioHubItemsController
{
    private readonly ICreateWorklistToBioHubItemHandler _createWorklistToBioHubItemHandler;
    private readonly IReadWorklistToBioHubItemHandler _readWorklistToBioHubItemHandler;
    private readonly IUpdateWorklistToBioHubItemHandler _updateWorklistToBioHubItemHandler;
    private readonly IUpdateWorklistToBioHubItemShipmentDocumentsHandler _updateWorklistToBioHubItemShipmentDocumentsHandler;
    private readonly IDeleteWorklistToBioHubItemHandler _deleteWorklistToBioHubItemHandler;
    private readonly IListWorklistToBioHubItemsHandler _listWorklistToBioHubItemsHandler;
    private readonly IListDashboardWorklistToBioHubItemsHandler _listDashboardWorklistToBioHubItemsHandler;
    private readonly ICreateUserFromUserRequestHandler _createUserFromUserRequestHandler;
    private readonly IAzureADUserInvitation _azureADUserInvitation;
    private readonly IDownloadWorklistToBioHubItemFileHandler _downloadWorklistToBioHubItemFileHandler;

    public WorklistToBioHubItemsController(
        ICreateWorklistToBioHubItemHandler createWorklistToBioHubItemHandler,
        IReadWorklistToBioHubItemHandler readWorklistToBioHubItemHandler,
        IUpdateWorklistToBioHubItemHandler updateWorklistToBioHubItemHandler,
        IUpdateWorklistToBioHubItemShipmentDocumentsHandler updateWorklistToBioHubItemShipmentDocumentsHandler,
        IDeleteWorklistToBioHubItemHandler deleteWorklistToBioHubItemHandler,
        IListWorklistToBioHubItemsHandler listWorklistToBioHubItemsHandler,
        IListDashboardWorklistToBioHubItemsHandler listDashboardWorklistToBioHubItemsHandler,
        IDownloadWorklistToBioHubItemFileHandler downloadWorklistToBioHubItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createWorklistToBioHubItemHandler = createWorklistToBioHubItemHandler;
        _readWorklistToBioHubItemHandler = readWorklistToBioHubItemHandler;
        _updateWorklistToBioHubItemHandler = updateWorklistToBioHubItemHandler;
        _updateWorklistToBioHubItemShipmentDocumentsHandler = updateWorklistToBioHubItemShipmentDocumentsHandler;
        _deleteWorklistToBioHubItemHandler = deleteWorklistToBioHubItemHandler;
        _listWorklistToBioHubItemsHandler = listWorklistToBioHubItemsHandler;
        _listDashboardWorklistToBioHubItemsHandler = listDashboardWorklistToBioHubItemsHandler;
        _downloadWorklistToBioHubItemFileHandler = downloadWorklistToBioHubItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateWorklistToBioHubItemCommand, Errors> body =
            await request.ParseBodyJson<CreateWorklistToBioHubItemCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();     
                
        var userLoginInfo = checkUserPermissionResult.Left;       

        if (body.Left.IsPast == true) {

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

        Either<CreateWorklistToBioHubItemCommandResponse, Errors> result =
            await _createWorklistToBioHubItemHandler.Handle(command, cancellationToken);
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

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<DeleteWorklistToBioHubItemCommandResponse, Errors> result =
            await _deleteWorklistToBioHubItemHandler.Handle(new(Id: id), cancellationToken);
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

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        var query = new ListWorklistToBioHubItemsQuery();       


        query.UserPermissions = userPermissions;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ListWorklistToBioHubItemsQueryResponse, Errors> result =
            await _listWorklistToBioHubItemsHandler.Handle(query, cancellationToken);

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

        var query = new ListDashboardWorklistToBioHubItemsQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        

        query.UserPermissions = userPermissions;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ListDashboardWorklistToBioHubItemsQueryResponse, Errors> result =
            await _listDashboardWorklistToBioHubItemsHandler.Handle(query, cancellationToken);

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

        var query = new ReadWorklistToBioHubItemQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        query.Id = id;
        query.UserPermissions = userPermissions;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ReadWorklistToBioHubItemQueryResponse, Errors> result =
            await _readWorklistToBioHubItemHandler.Handle(query, cancellationToken);

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

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        UpdateWorklistToBioHubItemCommand command = JsonConvert.DeserializeObject<UpdateWorklistToBioHubItemCommand>(request.Form["Command"][0]);

        command.UserId = userLoginInfo.UserId;

        IFormFile? file = request.Form.Files.Any() ? request.Form.Files.FirstOrDefault() : null;
        command.File = file;
        command.UserPermissions = userPermissions;
        command.RoleType = userLoginInfo?.RoleType;
        command.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        command.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<UpdateWorklistToBioHubItemCommandResponse, Errors> result =
            await _updateWorklistToBioHubItemHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> UpdateShipmentDocuments(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanSubmitSMTA1ShipmentDocuments) &&
            !userPermissions.Contains(PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        UpdateWorklistToBioHubItemShipmentDocumentsCommand command = JsonConvert.DeserializeObject<UpdateWorklistToBioHubItemShipmentDocumentsCommand>(request.Form["Command"][0]);

        command.UserId = userLoginInfo.UserId;

        IFormFile? file = request.Form.Files.Any() ? request.Form.Files.FirstOrDefault() : null;
        command.File = file;
        command.UserPermissions = userPermissions;
        command.RoleType = userLoginInfo?.RoleType;
        command.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        command.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<UpdateWorklistToBioHubItemShipmentDocumentsCommandResponse, Errors> result =
            await _updateWorklistToBioHubItemShipmentDocumentsHandler.Handle(command, cancellationToken);
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

        if (!userPermissions.Contains(PermissionNames.CanAccessWorklist) &&
            !userPermissions.Contains(PermissionNames.CanAccessPastWorklist))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        var query = new DownloadWorklistToBioHubItemFileQuery();

        query.Id = id;
        query.UserPermissions = userPermissions;
        query.WorklistId = worklistId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;


        Either<DownloadWorklistToBioHubItemFileQueryResponse, Errors> result =
            await _downloadWorklistToBioHubItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
