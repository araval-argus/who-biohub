using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.CreateInternationalTaxonomyClassification;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.DeleteInternationalTaxonomyClassification;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.ReadInternationalTaxonomyClassification;
using WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.UpdateInternationalTaxonomyClassification;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IInternationalTaxonomyClassificationsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class InternationalTaxonomyClassificationsController : BaseIdentityController, IInternationalTaxonomyClassificationsController
{
    private readonly ICreateInternationalTaxonomyClassificationHandler _createInternationalTaxonomyClassificationHandler;
    private readonly IReadInternationalTaxonomyClassificationHandler _readInternationalTaxonomyClassificationHandler;
    private readonly IUpdateInternationalTaxonomyClassificationHandler _updateInternationalTaxonomyClassificationHandler;
    private readonly IDeleteInternationalTaxonomyClassificationHandler _deleteInternationalTaxonomyClassificationHandler;
    private readonly IListInternationalTaxonomyClassificationsHandler _listInternationalTaxonomyClassificationsHandler;

    public InternationalTaxonomyClassificationsController(
        ICreateInternationalTaxonomyClassificationHandler createInternationalTaxonomyClassificationHandler,
        IReadInternationalTaxonomyClassificationHandler readInternationalTaxonomyClassificationHandler,
        IUpdateInternationalTaxonomyClassificationHandler updateInternationalTaxonomyClassificationHandler,
        IDeleteInternationalTaxonomyClassificationHandler deleteInternationalTaxonomyClassificationHandler,
        IListInternationalTaxonomyClassificationsHandler listInternationalTaxonomyClassificationsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createInternationalTaxonomyClassificationHandler = createInternationalTaxonomyClassificationHandler;
        _readInternationalTaxonomyClassificationHandler = readInternationalTaxonomyClassificationHandler;
        _updateInternationalTaxonomyClassificationHandler = updateInternationalTaxonomyClassificationHandler;
        _deleteInternationalTaxonomyClassificationHandler = deleteInternationalTaxonomyClassificationHandler;
        _listInternationalTaxonomyClassificationsHandler = listInternationalTaxonomyClassificationsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateInternationalTaxonomyClassification);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateInternationalTaxonomyClassificationCommand, Errors> body =
            await request.ParseBodyJson<CreateInternationalTaxonomyClassificationCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateInternationalTaxonomyClassificationCommandResponse, Errors> result =
            await _createInternationalTaxonomyClassificationHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteInternationalTaxonomyClassification);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteInternationalTaxonomyClassificationCommandResponse, Errors> result =
            await _deleteInternationalTaxonomyClassificationHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadInternationalTaxonomyClassification);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListInternationalTaxonomyClassificationsQueryResponse, Errors> result =
            await _listInternationalTaxonomyClassificationsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadInternationalTaxonomyClassification);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadInternationalTaxonomyClassificationQueryResponse, Errors> result =
            await _readInternationalTaxonomyClassificationHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditInternationalTaxonomyClassification);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateInternationalTaxonomyClassificationCommand, Errors> body =
            await request.ParseBodyJson<UpdateInternationalTaxonomyClassificationCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateInternationalTaxonomyClassificationCommandResponse, Errors> result =
            await _updateInternationalTaxonomyClassificationHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
