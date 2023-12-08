using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.CreateMaterial;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.DeleteMaterial;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterial;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterial;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForBioHubFacilityCompletion;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForLaboratoryCompletion;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForBioHubFacilityCompletion;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForLaboratoryCompletion;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialEvents.ListMaterialEvents;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IMaterialsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ListMaterialsForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken);
    Task<IActionResult> UpdateForBioHubFacilityCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> UpdateForLaboratoryCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadForBioHubFacilityCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadForLaboratoryCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ListMaterialsForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid worklistFromBioHubItemId, Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<IActionResult> ListEventsHandler(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
}

public class MaterialsController : BaseIdentityController, IMaterialsController
{
    private readonly ICreateMaterialHandler _createMaterialHandler;
    private readonly IReadMaterialHandler _readMaterialHandler;
    private readonly IUpdateMaterialHandler _updateMaterialHandler;
    private readonly IDeleteMaterialHandler _deleteMaterialHandler;
    private readonly IListMaterialsHandler _listMaterialsHandler;

    private readonly IListMaterialsForWorklistFromBioHubItemHandler _readMaterialsForWorklistFromBioHubItemHandler;
    private readonly IListMaterialsForWorklistToBioHubItemHandler _readMaterialsForWorklistToBioHubItemHandler;

    private readonly IUpdateMaterialForBioHubFacilityCompletionHandler _updateMaterialForBioHubFacilityCompletionHandler;

    private readonly IUpdateMaterialForLaboratoryCompletionHandler _updateMaterialForLaboratoryCompletionHandler;

    private readonly IReadMaterialForBioHubFacilityCompletionHandler _readMaterialForBioHubFacilityCompletionHandler;

    private readonly IReadMaterialForLaboratoryCompletionHandler _readMaterialForLaboratoryCompletionHandler;
    private readonly IListMaterialEventsHandler _listMaterialEventsHandler;

    public MaterialsController(
        ICreateMaterialHandler createMaterialHandler,
        IReadMaterialHandler readMaterialHandler,
        IUpdateMaterialHandler updateMaterialHandler,
        IDeleteMaterialHandler deleteMaterialHandler,
        IListMaterialsHandler listMaterialsHandler,
        IListMaterialsForWorklistFromBioHubItemHandler readMaterialsForWorklistFromBioHubItemHandler,
        IListMaterialsForWorklistToBioHubItemHandler readMaterialsForWorklistToBioHubItemHandler,
        IUpdateMaterialForBioHubFacilityCompletionHandler updateMaterialForBioHubFacilityCompletionHandler,
        IUpdateMaterialForLaboratoryCompletionHandler updateMaterialForLaboratoryCompletionHandler,
        IReadMaterialForBioHubFacilityCompletionHandler readMaterialForBioHubFacilityCompletionHandler,
        IReadMaterialForLaboratoryCompletionHandler readMaterialForLaboratoryCompletionHandler,
        IListMaterialEventsHandler listMaterialEventsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation
        ) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createMaterialHandler = createMaterialHandler;
        _readMaterialHandler = readMaterialHandler;
        _updateMaterialHandler = updateMaterialHandler;
        _deleteMaterialHandler = deleteMaterialHandler;
        _listMaterialsHandler = listMaterialsHandler;
        _readMaterialsForWorklistFromBioHubItemHandler = readMaterialsForWorklistFromBioHubItemHandler;
        _readMaterialsForWorklistToBioHubItemHandler = readMaterialsForWorklistToBioHubItemHandler;

        _updateMaterialForBioHubFacilityCompletionHandler = updateMaterialForBioHubFacilityCompletionHandler;
        _updateMaterialForLaboratoryCompletionHandler = updateMaterialForLaboratoryCompletionHandler;

        _readMaterialForBioHubFacilityCompletionHandler = readMaterialForBioHubFacilityCompletionHandler;

        _readMaterialForLaboratoryCompletionHandler = readMaterialForLaboratoryCompletionHandler;
        _listMaterialEventsHandler = listMaterialEventsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateMaterialCommand, Errors> body =
            await request.ParseBodyJson<CreateMaterialCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        if (userLoginInfo.RoleType == RoleType.Laboratory && userLoginInfo.LaboratoryId != body.Left.ProviderLaboratoryId)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<CreateMaterialCommandResponse, Errors> result =
            await _createMaterialHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<DeleteMaterialCommandResponse, Errors> result =
            await _deleteMaterialHandler.Handle(new(Id: id, RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId, OperationById: checkUserPermissionResult.Left.UserId), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListMaterialsQueryResponse, Errors> result =
            await _listMaterialsHandler.Handle(new(RoleType: userLoginInfo.RoleType, LaboratoryId: userLoginInfo.LaboratoryId, BioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ReadMaterialQueryResponse, Errors> result =
            await _readMaterialHandler.Handle(new(Id: id, RoleType: userLoginInfo.RoleType, UserLaboratoryId: userLoginInfo.LaboratoryId, UserBioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);


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

        if (!(userPermissions.Contains(PermissionNames.CanEditMaterial) ||
            userPermissions.Contains(PermissionNames.CanApproveBioHubFacilityCompletion) ||
            userPermissions.Contains(PermissionNames.CanSetMaterialReadyToShare) ||
            userPermissions.Contains(PermissionNames.CanSetMaterialPublic) ||
            userPermissions.Contains(PermissionNames.CanAddMaterialNewVials) ||
            userPermissions.Contains(PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold) ||
            userPermissions.Contains(PermissionNames.CanEditMaterialOwnerBioHubFacility)))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }


        Either<UpdateMaterialCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();


        UpdateMaterialCommand command = new UpdateMaterialCommand();

        command = body.Left;
        command.UserId = userLoginInfo.UserId;
        command.UserPermissions = userPermissions;

        if (userLoginInfo.RoleType == RoleType.Laboratory && userLoginInfo.LaboratoryId != body.Left.ProviderLaboratoryId)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (userLoginInfo.RoleType == RoleType.BioHubFacility && userLoginInfo.BioHubFacilityId != body.Left.OwnerBioHubFacilityId)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (command.NumberOfVialsToAdd != null && command.NumberOfVialsToAdd != 0 && !userPermissions.Contains(PermissionNames.CanAddMaterialNewVials))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<UpdateMaterialCommandResponse, Errors> result =
            await _updateMaterialHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListMaterialsForWorklistFromBioHubItem(HttpRequest request, ILogger log, Guid worklistFromBioHubItemId, Guid bioHubFacilityId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<ListMaterialsForWorklistFromBioHubItemQueryResponse, Errors> result =
            await _readMaterialsForWorklistFromBioHubItemHandler.Handle(new(WorklistFromBioHubItemId: worklistFromBioHubItemId, BioHubFacilityId: bioHubFacilityId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> ListMaterialsForWorklistToBioHubItem(HttpRequest request, ILogger log, Guid worklistToBioHubItemId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<ListMaterialsForWorklistToBioHubItemQueryResponse, Errors> result =
            await _readMaterialsForWorklistToBioHubItemHandler.Handle(new(WorklistToBioHubItemId: worklistToBioHubItemId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> UpdateForBioHubFacilityCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        Either<UpdateMaterialForBioHubFacilityCompletionCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialForBioHubFacilityCompletionCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        if (!(userPermissions.Contains(PermissionNames.CanEditMaterial) ||
            userPermissions.Contains(PermissionNames.CanApproveBioHubFacilityCompletion) ||
            userPermissions.Contains(PermissionNames.CanSetMaterialReadyToShare) ||
            userPermissions.Contains(PermissionNames.CanSetMaterialPublic) ||
            userPermissions.Contains(PermissionNames.CanAddMaterialNewVials) ||
            userPermissions.Contains(PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold) ||
            userPermissions.Contains(PermissionNames.CanEditMaterialOwnerBioHubFacility))
            )
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (userLoginInfo.RoleType == RoleType.Laboratory && userLoginInfo.LaboratoryId != body.Left.ProviderLaboratoryId)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (body.Left.Approve == true && !userPermissions.Contains(PermissionNames.CanApproveBioHubFacilityCompletion))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }


        if (userLoginInfo.RoleType == RoleType.BioHubFacility && userLoginInfo.BioHubFacilityId != body.Left.OwnerBioHubFacilityId)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (body.Left.NumberOfVialsToAdd != null && body.Left.NumberOfVialsToAdd != 0 && !userPermissions.Contains(PermissionNames.CanAddMaterialNewVials))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        UpdateMaterialForBioHubFacilityCompletionCommand command = new UpdateMaterialForBioHubFacilityCompletionCommand();

        command = body.Left;
        command.UserId = userLoginInfo.UserId;
        command.UserPermissions = userPermissions;

        Either<UpdateMaterialForBioHubFacilityCompletionCommandResponse, Errors> result =
            await _updateMaterialForBioHubFacilityCompletionHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> UpdateForLaboratoryCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        if (!(userPermissions.Contains(PermissionNames.CanEditMaterial) ||
            userPermissions.Contains(PermissionNames.CanApproveLaboratoryCompletion) ||
            userPermissions.Contains(PermissionNames.CanVerifyMaterial) ||
            userPermissions.Contains(PermissionNames.CanSetMaterialReadyToShare) ||
            userPermissions.Contains(PermissionNames.CanSetMaterialPublic) ||
            userPermissions.Contains(PermissionNames.CanAddMaterialNewVials) ||
            userPermissions.Contains(PermissionNames.CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold) ||
            userPermissions.Contains(PermissionNames.CanEditMaterialOwnerBioHubFacility)))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        Either<UpdateMaterialForLaboratoryCompletionCommand, Errors> body =
            await request.ParseBodyJson<UpdateMaterialForLaboratoryCompletionCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        if (userLoginInfo.RoleType == RoleType.Laboratory && userLoginInfo.LaboratoryId != body.Left.ProviderLaboratoryId)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (body.Left.Approve == true && !userPermissions.Contains(PermissionNames.CanApproveLaboratoryCompletion))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (userLoginInfo.RoleType == RoleType.BioHubFacility && userLoginInfo.BioHubFacilityId != body.Left.OwnerBioHubFacilityId)
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }

        if (body.Left.NumberOfVialsToAdd != null && body.Left.NumberOfVialsToAdd != 0 && !userPermissions.Contains(PermissionNames.CanAddMaterialNewVials))
        {
            return (new Errors(ErrorType.Unauthorized, "unauthorized")).ToIActionResult();
        }


        UpdateMaterialForLaboratoryCompletionCommand command = new UpdateMaterialForLaboratoryCompletionCommand();

        command = body.Left;
        command.UserId = userLoginInfo.UserId;
        command.UserPermissions = userPermissions;


        Either<UpdateMaterialForLaboratoryCompletionCommandResponse, Errors> result =
            await _updateMaterialForLaboratoryCompletionHandler.Handle(command, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadForBioHubFacilityCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ReadMaterialForBioHubFacilityCompletionQueryResponse, Errors> result =
            await _readMaterialForBioHubFacilityCompletionHandler.Handle(new(Id: id, RoleType: userLoginInfo.RoleType, UserLaboratoryId: userLoginInfo.LaboratoryId, UserBioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);


        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }



    public async Task<IActionResult> ReadForLaboratoryCompletion(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ReadMaterialForLaboratoryCompletionQueryResponse, Errors> result =
            await _readMaterialForLaboratoryCompletionHandler.Handle(new(Id: id, RoleType: userLoginInfo.RoleType, UserLaboratoryId: userLoginInfo.LaboratoryId, UserBioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);


        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListEventsHandler(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadMaterial);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListMaterialEventsQueryResponse, Errors> result =
            await _listMaterialEventsHandler.Handle(new(Id: id, RoleType: userLoginInfo.RoleType, UserLaboratoryId: userLoginInfo.LaboratoryId, UserBioHubFacilityId: userLoginInfo.BioHubFacilityId), cancellationToken);


        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
