using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.CreateBookingForm;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.DeleteBookingForm;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ListBookingForms;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.ReadBookingForm;
using WHO.BioHub.DataManagement.Core.UseCases.BookingForms.UpdateBookingForm;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;
using WHO.BioHub.Identity;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.API.Http.Controllers;

public interface IBookingFormsController
{
    Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, ILogger log, Guid courierId, CancellationToken cancellationToken);
}

public class BookingFormsController : BaseIdentityController, IBookingFormsController
{
    private readonly ICreateBookingFormHandler _createBookingFormHandler;
    private readonly IReadBookingFormHandler _readBookingFormHandler;
    private readonly IUpdateBookingFormHandler _updateBookingFormHandler;
    private readonly IDeleteBookingFormHandler _deleteBookingFormHandler;
    private readonly IListBookingFormsHandler _listBookingFormsHandler;

    public BookingFormsController(
        ICreateBookingFormHandler createBookingFormHandler,
        IReadBookingFormHandler readBookingFormHandler,
        IUpdateBookingFormHandler updateBookingFormHandler,
        IDeleteBookingFormHandler deleteBookingFormHandler,
        IListBookingFormsHandler listBookingFormsHandler,
        IGetAccessInformationHandler getAccessInformationHandler,
        IAzureADTokenValidation azureADTokenValidation) : base(getAccessInformationHandler, azureADTokenValidation)
    {
        _createBookingFormHandler = createBookingFormHandler;
        _readBookingFormHandler = readBookingFormHandler;
        _updateBookingFormHandler = updateBookingFormHandler;
        _deleteBookingFormHandler = deleteBookingFormHandler;
        _listBookingFormsHandler = listBookingFormsHandler;
    }

    public async Task<IActionResult> Create(HttpRequest request, ILogger log, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanCreateCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<CreateBookingFormCommand, Errors> body =
            await request.ParseBodyJson<CreateBookingFormCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<CreateBookingFormCommandResponse, Errors> result =
            await _createBookingFormHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Delete(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanDeleteCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<DeleteBookingFormCommandResponse, Errors> result =
            await _deleteBookingFormHandler.Handle(new(Id: id), cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> List(HttpRequest request, ILogger log, Guid courierId, CancellationToken cancellationToken)
    {

        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ListBookingFormsQueryResponse, Errors> result =
            await _listBookingFormsHandler.Handle(new(CourierId: courierId), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanReadCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<ReadBookingFormQueryResponse, Errors> result =
            await _readBookingFormHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, ILogger log, Guid id, CancellationToken cancellationToken)
    {
        Either<UserLoginInfo, Errors> checkUserPermissionResult =
            await CheckUserPermission(request, log, cancellationToken, PermissionNames.CanEditCourier);
        if (checkUserPermissionResult.IsRight)
            return checkUserPermissionResult.Right.ToIActionResult();

        Either<UpdateBookingFormCommand, Errors> body =
            await request.ParseBodyJson<UpdateBookingFormCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateBookingFormCommandResponse, Errors> result =
            await _updateBookingFormHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
