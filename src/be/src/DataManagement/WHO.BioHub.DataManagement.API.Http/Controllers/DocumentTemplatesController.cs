using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UploadFile;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.DeleteDocumentTemplate;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListDocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadFile;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UpdateDocumentTemplate;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CreateFolder;
using Newtonsoft.Json;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckOtherCurrentPresent;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckCurrentsForDelete;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ListSMTADocumentTemplates;
using WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadEFormFile;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IDocumentTemplatesController
{
    Task<IActionResult> CreateFolder(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> UploadFile(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> CheckOtherCurrentPresent(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> FolderContainsCurrent(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ListSMTA(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> ReadEFormFile(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
}

public class DocumentTemplatesController : BaseIdentityController, IDocumentTemplatesController
{
    private readonly IUploadFileHandler _uploadFileHandler;
    private readonly ICreateFolderHandler _createFolderHandler;
    private readonly IReadFileHandler _readFileHandler;
    private readonly IUpdateDocumentTemplateHandler _updateNameHandler;
    private readonly IDeleteDocumentTemplateHandler _deleteDocumentTemplateHandler;
    private readonly IListDocumentTemplatesHandler _listDocumentTemplatesHandler;
    private readonly ICheckOtherCurrentPresentHandler _checkOtherCurrentPresentHandler;
    private readonly ICheckCurrentsForDeleteHandler _checkCurrentsForDeleteHandler;
    private readonly IListSMTADocumentTemplatesHandler _listSMTADocumentTemplatesHandler;
    private readonly IReadEFormFileHandler _readEFormFileHandler;

    public DocumentTemplatesController(
        IUploadFileHandler uploadFileHandler,
        ICreateFolderHandler createFolderHandler,
        IReadFileHandler readFileHandler,
        IUpdateDocumentTemplateHandler updateNameHandler,
        IDeleteDocumentTemplateHandler deleteDocumentTemplateHandler,
        IListDocumentTemplatesHandler listDocumentTemplatesHandler,
        ICheckOtherCurrentPresentHandler checkOtherCurrentPresentHandler,
        ICheckCurrentsForDeleteHandler checkCurrentsForDeleteHandler,
        IListSMTADocumentTemplatesHandler listSMTADocumentTemplatesHandler,
        IReadEFormFileHandler readEFormFileHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _uploadFileHandler = uploadFileHandler;
        _readFileHandler = readFileHandler;
        _updateNameHandler = updateNameHandler;
        _deleteDocumentTemplateHandler = deleteDocumentTemplateHandler;
        _listDocumentTemplatesHandler = listDocumentTemplatesHandler;
        _createFolderHandler = createFolderHandler;
        _checkOtherCurrentPresentHandler = checkOtherCurrentPresentHandler;
        _checkCurrentsForDeleteHandler = checkCurrentsForDeleteHandler;
        _listSMTADocumentTemplatesHandler = listSMTADocumentTemplatesHandler;
        _readEFormFileHandler = readEFormFileHandler;
    }

    public async Task<IActionResult> UploadFile(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        var uploadedById = userLoginInfo.UserId;
        IFormFile? file = request.Form.Files.FirstOrDefault();

        UploadFileCommand uploadFileCommand = JsonConvert.DeserializeObject<UploadFileCommand>(request.Form["Data"][0]);
        uploadFileCommand.File = file;
        uploadFileCommand.UploadedById = uploadedById;

        Either<UploadFileCommandResponse, Errors> result =
            await _uploadFileHandler.Handle(uploadFileCommand, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();


        return new OkObjectResult(result.Left);
    }


    public async Task<IActionResult> CreateFolder(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateFolderCommand, Errors> body =
            await request.ParseBodyJson<CreateFolderCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateFolderCommandResponse, Errors> result =
            await _createFolderHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteDocumentTemplateCommandResponse, Errors> result =
            await _deleteDocumentTemplateHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<ListDocumentTemplatesQueryResponse, Errors> result =
            await _listDocumentTemplatesHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ListSMTA(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        var userLoginInfo = checkUserPermissionResult.Left;

        Either<ListSMTADocumentTemplatesQueryResponse, Errors> result =
            await _listSMTADocumentTemplatesHandler.Handle(new(LaboratoryId: userLoginInfo.LaboratoryId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadFileQueryResponse, Errors> result =
            await _readFileHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }

    public async Task<IActionResult> ReadEFormFile(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadEForm);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<ReadEFormFileQueryResponse, Errors> result =
            await _readEFormFileHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left.DownloadedFile);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateDocumentTemplateCommand, Errors> body =
            await request.ParseBodyJson<UpdateDocumentTemplateCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateDocumentTemplateCommandResponse, Errors> result =
            await _updateNameHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> CheckOtherCurrentPresent(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CheckOtherCurrentPresentQueryResponse, Errors> result =
            await _checkOtherCurrentPresentHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> FolderContainsCurrent(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteDocumentTemplate);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();


        Either<CheckCurrentsForDeleteQueryResponse, Errors> result =
            await _checkCurrentsForDeleteHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
