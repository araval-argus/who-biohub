using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.DeleteGeneticSequenceData;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ReadGeneticSequenceData;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IGeneticSequenceDatasController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken);
}

public class GeneticSequenceDatasController : BaseIdentityController, IGeneticSequenceDatasController
{
    private readonly ICreateGeneticSequenceDataHandler _createGeneticSequenceDataHandler;
    private readonly IReadGeneticSequenceDataHandler _readGeneticSequenceDataHandler;
    private readonly IUpdateGeneticSequenceDataHandler _updateGeneticSequenceDataHandler;
    private readonly IDeleteGeneticSequenceDataHandler _deleteGeneticSequenceDataHandler;
    private readonly IListGeneticSequenceDatasHandler _listGeneticSequenceDatasHandler;

    public GeneticSequenceDatasController(
        ICreateGeneticSequenceDataHandler createGeneticSequenceDataHandler,
        IReadGeneticSequenceDataHandler readGeneticSequenceDataHandler,
        IUpdateGeneticSequenceDataHandler updateGeneticSequenceDataHandler,
        IDeleteGeneticSequenceDataHandler deleteGeneticSequenceDataHandler,
        IListGeneticSequenceDatasHandler listGeneticSequenceDatasHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)

    {
        _createGeneticSequenceDataHandler = createGeneticSequenceDataHandler;
        _readGeneticSequenceDataHandler = readGeneticSequenceDataHandler;
        _updateGeneticSequenceDataHandler = updateGeneticSequenceDataHandler;
        _deleteGeneticSequenceDataHandler = deleteGeneticSequenceDataHandler;
        _listGeneticSequenceDatasHandler = listGeneticSequenceDatasHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateGeneticSequenceData);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateGeneticSequenceDataCommand, Errors> body =
            await request.ParseBodyJson<CreateGeneticSequenceDataCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateGeneticSequenceDataCommandResponse, Errors> result =
            await _createGeneticSequenceDataHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteGeneticSequenceData);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteGeneticSequenceDataCommandResponse, Errors> result =
            await _deleteGeneticSequenceDataHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadGeneticSequenceData);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListGeneticSequenceDatasQueryResponse, Errors> result =
            await _listGeneticSequenceDatasHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadGeneticSequenceData);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadGeneticSequenceDataQueryResponse, Errors> result =
            await _readGeneticSequenceDataHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditGeneticSequenceData);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateGeneticSequenceDataCommand, Errors> body =
            await request.ParseBodyJson<UpdateGeneticSequenceDataCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateGeneticSequenceDataCommandResponse, Errors> result =
            await _updateGeneticSequenceDataHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
