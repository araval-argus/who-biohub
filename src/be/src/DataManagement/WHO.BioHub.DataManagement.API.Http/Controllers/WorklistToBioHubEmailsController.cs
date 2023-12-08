using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.DeleteWorklistToBioHubEmail;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ListWorklistToBioHubEmails;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ReadWorklistToBioHubEmail;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.UpdateWorklistToBioHubEmail;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IWorklistToBioHubEmailsController
{
    Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class WorklistToBioHubEmailsController : ControllerBase, IWorklistToBioHubEmailsController
{
    private readonly ICreateWorklistToBioHubEmailHandler _createWorklistToBioHubEmailHandler;
    private readonly IReadWorklistToBioHubEmailHandler _readWorklistToBioHubEmailHandler;
    private readonly IUpdateWorklistToBioHubEmailHandler _updateWorklistToBioHubEmailHandler;
    private readonly IDeleteWorklistToBioHubEmailHandler _deleteWorklistToBioHubEmailHandler;
    private readonly IListWorklistToBioHubEmailsHandler _listWorklistToBioHubEmailsHandler;

    public WorklistToBioHubEmailsController(
        ICreateWorklistToBioHubEmailHandler createWorklistToBioHubEmailHandler,
        IReadWorklistToBioHubEmailHandler readWorklistToBioHubEmailHandler,
        IUpdateWorklistToBioHubEmailHandler updateWorklistToBioHubEmailHandler,
        IDeleteWorklistToBioHubEmailHandler deleteWorklistToBioHubEmailHandler,
        IListWorklistToBioHubEmailsHandler listWorklistToBioHubEmailsHandler)
    {
        _createWorklistToBioHubEmailHandler = createWorklistToBioHubEmailHandler;
        _readWorklistToBioHubEmailHandler = readWorklistToBioHubEmailHandler;
        _updateWorklistToBioHubEmailHandler = updateWorklistToBioHubEmailHandler;
        _deleteWorklistToBioHubEmailHandler = deleteWorklistToBioHubEmailHandler;
        _listWorklistToBioHubEmailsHandler = listWorklistToBioHubEmailsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<CreateWorklistToBioHubEmailCommand, Errors> body =
            await request.ParseBodyJson<CreateWorklistToBioHubEmailCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateWorklistToBioHubEmailCommandResponse, Errors> result =
            await _createWorklistToBioHubEmailHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<DeleteWorklistToBioHubEmailCommandResponse, Errors> result =
            await _deleteWorklistToBioHubEmailHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListWorklistToBioHubEmailsQueryResponse, Errors> result =
            await _listWorklistToBioHubEmailsHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadWorklistToBioHubEmailQueryResponse, Errors> result =
            await _readWorklistToBioHubEmailHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<UpdateWorklistToBioHubEmailCommand, Errors> body =
            await request.ParseBodyJson<UpdateWorklistToBioHubEmailCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateWorklistToBioHubEmailCommandResponse, Errors> result =
            await _updateWorklistToBioHubEmailHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
