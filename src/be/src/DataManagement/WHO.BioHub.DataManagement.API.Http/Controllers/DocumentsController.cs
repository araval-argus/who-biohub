using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.CanStartSMTA2Request;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.CheckDocumentSigned;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.CreateDocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.DeleteDocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocuments;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocuments;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ReadDocument;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.UpdateDocument;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IDocumentsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> CheckDocumentSigned(HttpRequest request, ILogger log, DocumentFileType type, Guid laboratoryId, CancellationToken cancellationToken);
    Task<IActionResult> CanStartSMTARequest(HttpRequest request, ILogger log, DocumentFileType type, CancellationToken cancellationToken);
    Task<IActionResult> ListSignedSMTA(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class DocumentsController : BaseIdentityController, IDocumentsController
{
    private readonly ICreateDocumentHandler _createDocumentHandler;
    private readonly IReadDocumentHandler _readDocumentHandler;
    private readonly IUpdateDocumentHandler _updateDocumentHandler;
    private readonly IDeleteDocumentHandler _deleteDocumentHandler;
    private readonly IListDocumentsHandler _listDocumentsHandler;
    private readonly IListSignedSMTADocumentsHandler _listSignedSMTADocumentsHandler;

    private readonly ICheckDocumentSignedHandler _checkDocumentSignedHandler;
    private readonly ICanStartSMTARequestHandler _canStartSMTARequestHandler;

    public DocumentsController(
        ICreateDocumentHandler createDocumentHandler,
        IReadDocumentHandler readDocumentHandler,
        IUpdateDocumentHandler updateDocumentHandler,
        IDeleteDocumentHandler deleteDocumentHandler,
        IListDocumentsHandler listDocumentsHandler,
        IListSignedSMTADocumentsHandler listSignedSMTADocumentsHandler,
        ICheckDocumentSignedHandler checkDocumentSignedHandler,
        ICanStartSMTARequestHandler canStartSMTARequestHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createDocumentHandler = createDocumentHandler;
        _readDocumentHandler = readDocumentHandler;
        _updateDocumentHandler = updateDocumentHandler;
        _deleteDocumentHandler = deleteDocumentHandler;
        _listDocumentsHandler = listDocumentsHandler;
        _listSignedSMTADocumentsHandler = listSignedSMTADocumentsHandler;
        _checkDocumentSignedHandler = checkDocumentSignedHandler;
        _canStartSMTARequestHandler = canStartSMTARequestHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateDocumentCommand, Errors> body =
            await request.ParseBodyJson<CreateDocumentCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateDocumentCommandResponse, Errors> result =
            await _createDocumentHandler.Handle(body.Left, cancellationToken);
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

        Either<DeleteDocumentCommandResponse, Errors> result =
            await _deleteDocumentHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadDocument);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ListDocumentsQuery query = new ListDocumentsQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ListDocumentsQueryResponse, Errors> result =
            await _listDocumentsHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListSignedSMTA(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadDocument);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ListSignedSMTADocumentsQuery query = new ListSignedSMTADocumentsQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.BioHubFacilityId = userLoginInfo?.BioHubFacilityId;

        Either<ListSignedSMTADocumentsQueryResponse, Errors> result =
            await _listSignedSMTADocumentsHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadDocument);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        ReadDocumentQuery query = new ReadDocumentQuery();
        query.Id = id;

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;

        Either<ReadDocumentQueryResponse, Errors> result =
            await _readDocumentHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateDocumentCommand, Errors> body =
            await request.ParseBodyJson<UpdateDocumentCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateDocumentCommandResponse, Errors> result =
            await _updateDocumentHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> CheckDocumentSigned(HttpRequest request, ILogger log, DocumentFileType type, Guid laboratoryId, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        CheckDocumentSignedQuery query = new CheckDocumentSignedQuery();

        query.LaboratoryId = laboratoryId;
        query.Type = type;

        Either<CheckDocumentSignedQueryResponse, Errors> result =
            await _checkDocumentSignedHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> CanStartSMTARequest(HttpRequest request, ILogger log, DocumentFileType type, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanAccessSMTAWorkflow);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        CanStartSMTARequestQuery query = new CanStartSMTARequestQuery();

        var userLoginInfo = checkUserPermissionResult.Left;

        query.LaboratoryId = userLoginInfo?.LaboratoryId;
        query.Type = type;

        Either<CanStartSMTARequestQueryResponse, Errors> result =
            await _canStartSMTARequestHandler.Handle(query, cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
