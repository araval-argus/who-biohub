using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA1;
using WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA2;
using WHO.BioHub.Data.Core.UseCases.EForms.BiosafetyChecklistOfSMTA2;
using WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA1;
using WHO.BioHub.Data.Core.UseCases.EForms.BookingFormOfSMTA2;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForms;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IEFormsController
{
    Task<IActionResult> ListEforms(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ReadAnnex2OfSMTA1(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadAnnex2OfSMTA2(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadBiosafetyChecklistOfSMTA2(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadBookingFormOfSMTA1(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadBookingFormOfSMTA2(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
}

public class EFormsController : BaseIdentityController, IEFormsController
{
    private readonly IListEFormsHandler _listEFormsHandler;
    private readonly IReadAnnex2OfSMTA1Handler _readAnnex2OfSMTA1Handler;
    private readonly IReadBookingFormOfSMTA1Handler _readBookingFormOfSMTA1Handler;
    private readonly IReadAnnex2OfSMTA2Handler _readAnnex2OfSMTA2Handler;
    private readonly IReadBiosafetyChecklistOfSMTA2Handler _readBiosafetyChecklistOfSMTA2Handler;
    private readonly IReadBookingFormOfSMTA2Handler _readBookingFormOfSMTA2Handler;


    public EFormsController(
        IListEFormsHandler listEFormsHandler,
       IReadAnnex2OfSMTA1Handler readAnnex2OfSMTA1Handler,
        IReadBookingFormOfSMTA1Handler readBookingFormOfSMTA1Handler,
        IReadAnnex2OfSMTA2Handler readAnnex2OfSMTA2Handler,
        IReadBiosafetyChecklistOfSMTA2Handler readBiosafetyChecklistOfSMTA2Handler,
        IReadBookingFormOfSMTA2Handler readBookingFormOfSMTA2Handler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _listEFormsHandler = listEFormsHandler;
        _readAnnex2OfSMTA1Handler = readAnnex2OfSMTA1Handler;
        _readBookingFormOfSMTA1Handler = readBookingFormOfSMTA1Handler;
        _readAnnex2OfSMTA2Handler = readAnnex2OfSMTA2Handler;
        _readBiosafetyChecklistOfSMTA2Handler = readBiosafetyChecklistOfSMTA2Handler;
        _readBookingFormOfSMTA2Handler = readBookingFormOfSMTA2Handler;

    }
    public async Task<IActionResult> ListEforms(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadEForm);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ListEFormsQuery query = new ListEFormsQuery();
        

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.RoleType = userLoginInfo?.RoleType;


        Either<ListEFormsQueryResponse, Errors> result =
            await _listEFormsHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> ReadAnnex2OfSMTA1(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadEForm);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ReadAnnex2OfSMTA1Query query = new ReadAnnex2OfSMTA1Query();
        query.WorklistId = id;

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.RoleType = userLoginInfo?.RoleType;

        Either<ReadAnnex2OfSMTA1QueryResponse, Errors> result =
            await _readAnnex2OfSMTA1Handler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadBookingFormOfSMTA1(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadEForm);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ReadBookingFormOfSMTA1Query query = new ReadBookingFormOfSMTA1Query();
        query.WorklistId = id;

        var userLoginInfo = checkUserPermissionResult.Left;
        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserPermissions = userPermissions;

        Either<ReadBookingFormOfSMTA1QueryResponse, Errors> result =
            await _readBookingFormOfSMTA1Handler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadAnnex2OfSMTA2(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadEForm);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ReadAnnex2OfSMTA2Query query = new ReadAnnex2OfSMTA2Query();
        query.WorklistId = id;

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.RoleType = userLoginInfo?.RoleType;

        Either<ReadAnnex2OfSMTA2QueryResponse, Errors> result =
            await _readAnnex2OfSMTA2Handler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> ReadBiosafetyChecklistOfSMTA2(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadEForm);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ReadBiosafetyChecklistOfSMTA2Query query = new ReadBiosafetyChecklistOfSMTA2Query();
        query.WorklistId = id;

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.RoleType = userLoginInfo?.RoleType;

        Either<ReadBiosafetyChecklistOfSMTA2QueryResponse, Errors> result =
            await _readBiosafetyChecklistOfSMTA2Handler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadBookingFormOfSMTA2(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadEForm);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ReadBookingFormOfSMTA2Query query = new ReadBookingFormOfSMTA2Query();
        query.WorklistId = id;

        var userLoginInfo = checkUserPermissionResult.Left;

        var userPermissions = userLoginInfo?.UserPermissions.Select(x => x.PermissionName);

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;
        query.RoleType = userLoginInfo?.RoleType;
        query.UserPermissions = userPermissions;

        Either<ReadBookingFormOfSMTA2QueryResponse, Errors> result =
            await _readBookingFormOfSMTA2Handler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
