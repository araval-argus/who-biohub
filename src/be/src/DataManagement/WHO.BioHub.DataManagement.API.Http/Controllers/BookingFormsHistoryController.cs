using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.CreateBookingFormHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.DeleteBookingFormHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ListBookingFormsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.ReadBookingFormHistory;
using WHO.BioHub.DataManagement.Core.UseCases.BookingFormsHistory.UpdateBookingFormHistory;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IBookingFormsHistoryController
{
    Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class BookingFormsHistoryController : ControllerBase, IBookingFormsHistoryController
{
    private readonly ICreateBookingFormHistoryHandler _createBookingFormHistoryHandler;
    private readonly IReadBookingFormHistoryHandler _readBookingFormHistoryHandler;
    private readonly IUpdateBookingFormHistoryHandler _updateBookingFormHistoryHandler;
    private readonly IDeleteBookingFormHistoryHandler _deleteBookingFormHistoryHandler;
    private readonly IListBookingFormsHistoryHandler _listBookingFormsHistoryHandler;

    public BookingFormsHistoryController(
        ICreateBookingFormHistoryHandler createBookingFormHistoryHandler,
        IReadBookingFormHistoryHandler readBookingFormHistoryHandler,
        IUpdateBookingFormHistoryHandler updateBookingFormHistoryHandler,
        IDeleteBookingFormHistoryHandler deleteBookingFormHistoryHandler,
        IListBookingFormsHistoryHandler listBookingFormsHistoryHandler)
    {
        _createBookingFormHistoryHandler = createBookingFormHistoryHandler;
        _readBookingFormHistoryHandler = readBookingFormHistoryHandler;
        _updateBookingFormHistoryHandler = updateBookingFormHistoryHandler;
        _deleteBookingFormHistoryHandler = deleteBookingFormHistoryHandler;
        _listBookingFormsHistoryHandler = listBookingFormsHistoryHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<CreateBookingFormHistoryCommand, Errors> body =
            await request.ParseBodyJson<CreateBookingFormHistoryCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateBookingFormHistoryCommandResponse, Errors> result =
            await _createBookingFormHistoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<DeleteBookingFormHistoryCommandResponse, Errors> result =
            await _deleteBookingFormHistoryHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListBookingFormsHistoryQueryResponse, Errors> result =
            await _listBookingFormsHistoryHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadBookingFormHistoryQueryResponse, Errors> result =
            await _readBookingFormHistoryHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<UpdateBookingFormHistoryCommand, Errors> body =
            await request.ParseBodyJson<UpdateBookingFormHistoryCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateBookingFormHistoryCommandResponse, Errors> result =
            await _updateBookingFormHistoryHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
