using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DeleteWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItem;
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
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DownloadWorklistFromBioHubItemFile;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemBHFShipmentDocuments;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemQEShipmentDocuments;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IWorklistFromBioHubItemsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> DownloadFile(HttpRequest request, ILogger log, Guid id, Guid worklistId, CancellationToken cancellationToken);
    Task<IActionResult> ListForDashboard(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> UpdateBHFShipmentDocuments(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> UpdateQEShipmentDocuments(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
}

public class WorklistFromBioHubItemsController : BaseIdentityController, IWorklistFromBioHubItemsController
{
    private readonly ICreateWorklistFromBioHubItemHandler _createWorklistFromBioHubItemHandler;
    private readonly IReadWorklistFromBioHubItemHandler _readWorklistFromBioHubItemHandler;
    private readonly IUpdateWorklistFromBioHubItemHandler _updateWorklistFromBioHubItemHandler;
    private readonly IUpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler _updateWorklistFromBioHubItemBHFShipmentDocumentsHandler;
    private readonly IUpdateWorklistFromBioHubItemQEShipmentDocumentsHandler _updateWorklistFromBioHubItemQEShipmentDocumentsHandler;
    private readonly IDeleteWorklistFromBioHubItemHandler _deleteWorklistFromBioHubItemHandler;
    private readonly IListWorklistFromBioHubItemsHandler _listWorklistFromBioHubItemsHandler;
    private readonly IListDashboardWorklistFromBioHubItemsHandler _listDashboardWorklistFromBioHubItemsHandler;
    private readonly ICreateUserFromUserRequestHandler _createUserFromUserRequestHandler;
    private readonly IAzureADUserInvitation _azureADUserInvitation;
    private readonly IDownloadWorklistFromBioHubItemFileHandler _downloadWorklistFromBioHubItemFileHandler;

    public WorklistFromBioHubItemsController(
        ICreateWorklistFromBioHubItemHandler createWorklistFromBioHubItemHandler,
        IReadWorklistFromBioHubItemHandler readWorklistFromBioHubItemHandler,
        IUpdateWorklistFromBioHubItemHandler updateWorklistFromBioHubItemHandler,
        IUpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler updateWorklistFromBioHubItemBHFShipmentDocumentsHandler,
        IUpdateWorklistFromBioHubItemQEShipmentDocumentsHandler updateWorklistFromBioHubItemQEShipmentDocumentsHandler,
        IDeleteWorklistFromBioHubItemHandler deleteWorklistFromBioHubItemHandler,
        IListWorklistFromBioHubItemsHandler listWorklistFromBioHubItemsHandler,
        IListDashboardWorklistFromBioHubItemsHandler listDashboardWorklistFromBioHubItemsHandler,
        IDownloadWorklistFromBioHubItemFileHandler downloadWorklistFromBioHubItemFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createWorklistFromBioHubItemHandler = createWorklistFromBioHubItemHandler;
        _readWorklistFromBioHubItemHandler = readWorklistFromBioHubItemHandler;
        _updateWorklistFromBioHubItemHandler = updateWorklistFromBioHubItemHandler;
        _updateWorklistFromBioHubItemBHFShipmentDocumentsHandler = updateWorklistFromBioHubItemBHFShipmentDocumentsHandler;
        _updateWorklistFromBioHubItemQEShipmentDocumentsHandler = updateWorklistFromBioHubItemQEShipmentDocumentsHandler;
        _deleteWorklistFromBioHubItemHandler = deleteWorklistFromBioHubItemHandler;
        _listWorklistFromBioHubItemsHandler = listWorklistFromBioHubItemsHandler;
        _listDashboardWorklistFromBioHubItemsHandler = listDashboardWorklistFromBioHubItemsHandler;
        _downloadWorklistFromBioHubItemFileHandler = downloadWorklistFromBioHubItemFileHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateWorklistFromBioHubItemCommand, Errors> body =
            await request.ParseBodyJson<CreateWorklistFromBioHubItemCommand>(cancellationToken);
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


        Either<CreateWorklistFromBioHubItemCommandResponse, Errors> result =
            await _createWorklistFromBioHubItemHandler.Handle(command, cancellationToken);
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

        Either<DeleteWorklistFromBioHubItemCommandResponse, Errors> result =
            await _deleteWorklistFromBioHubItemHandler.Handle(new(Id: id), cancellationToken);
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

        var query = new ListWorklistFromBioHubItemsQuery();

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

        Either<ListWorklistFromBioHubItemsQueryResponse, Errors> result =
            await _listWorklistFromBioHubItemsHandler.Handle(query, cancellationToken);

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

        var query = new ListDashboardWorklistFromBioHubItemsQuery();

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

        Either<ListDashboardWorklistFromBioHubItemsQueryResponse, Errors> result =
            await _listDashboardWorklistFromBioHubItemsHandler.Handle(query, cancellationToken);

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

        var query = new ReadWorklistFromBioHubItemQuery();

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

        Either<ReadWorklistFromBioHubItemQueryResponse, Errors> result =
            await _readWorklistFromBioHubItemHandler.Handle(query, cancellationToken);

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

        UpdateWorklistFromBioHubItemCommand command = JsonConvert.DeserializeObject<UpdateWorklistFromBioHubItemCommand>(request.Form["Command"][0]);

        command.UserId = userLoginInfo.UserId;

        IFormFile? file = request.Form.Files.Any() ? request.Form.Files.FirstOrDefault() : null;
        command.File = file;
        command.UserPermissions = userPermissions;
        command.RoleType = userLoginInfo?.RoleType;
        command.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        command.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<UpdateWorklistFromBioHubItemCommandResponse, Errors> result =
            await _updateWorklistFromBioHubItemHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> UpdateBHFShipmentDocuments(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);        

        if (!userPermissions.Contains(PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments) &&
            !userPermissions.Contains(PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand command = JsonConvert.DeserializeObject<UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand>(request.Form["Command"][0]);

        command.UserId = userLoginInfo.UserId;

        IFormFile? file = request.Form.Files.Any() ? request.Form.Files.FirstOrDefault() : null;
        command.File = file;
        command.UserPermissions = userPermissions;
        command.RoleType = userLoginInfo?.RoleType;
        command.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        command.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandResponse, Errors> result =
            await _updateWorklistFromBioHubItemBHFShipmentDocumentsHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> UpdateQEShipmentDocuments(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!userPermissions.Contains(PermissionNames.CanSubmitQESMTA2ShipmentDocuments) &&
            !userPermissions.Contains(PermissionNames.CanSubmitQESMTA2ShipmentDocumentsPast))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand command = JsonConvert.DeserializeObject<UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand>(request.Form["Command"][0]);

        command.UserId = userLoginInfo.UserId;

        IFormFile? file = request.Form.Files.Any() ? request.Form.Files.FirstOrDefault() : null;
        command.File = file;
        command.UserPermissions = userPermissions;
        command.RoleType = userLoginInfo?.RoleType;
        command.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        command.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<UpdateWorklistFromBioHubItemQEShipmentDocumentsCommandResponse, Errors> result =
            await _updateWorklistFromBioHubItemQEShipmentDocumentsHandler.Handle(command, cancellationToken);
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

        var query = new DownloadWorklistFromBioHubItemFileQuery();

        query.Id = id;
        query.UserPermissions = userPermissions;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserLaboratoryId = userLoginInfo?.LaboratoryId;
        query.UserBioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.WorklistId = worklistId;


        Either<DownloadWorklistFromBioHubItemFileQueryResponse, Errors> result =
            await _downloadWorklistFromBioHubItemFileHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }
}
