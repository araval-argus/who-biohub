using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.PublicData.Core.UseCases.UserRequests.CreateUserRequest;
using WHO.BioHub.PublicData.Core.UseCases.UserRequests.ReadUserRequest;
using WHO.BioHub.PublicData.Core.UseCases.UserRequests.UpdateUserRequest;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Captcha;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IUserRequestsController
{
    Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken);
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken);
}

public class UserRequestsController : ControllerBase, IUserRequestsController
{
    private readonly ICreateUserRequestHandler _createUserRequestHandler;
    private readonly IReadUserRequestHandler _readUserRequestHandler;
    private readonly IUpdateUserRequestHandler _updateUserRequestHandler;
    private readonly ICaptcha _captcha;

    public UserRequestsController(
        ICreateUserRequestHandler createUserRequestHandler,
        IReadUserRequestHandler readUserRequestHandler,
        IUpdateUserRequestHandler updateUserRequestHandler,
        ICaptcha captcha
        )
    {
        _createUserRequestHandler = createUserRequestHandler;
        _readUserRequestHandler = readUserRequestHandler;
        _updateUserRequestHandler = updateUserRequestHandler;
        _captcha = captcha;

    }

    public async Task<IActionResult> Create(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<CreateUserRequestCommand, Errors> body =
            await request.ParseBodyJson<CreateUserRequestCommand>(cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        var captchaVerifyResult = await _captcha.Verify(body.Left.RecaptchaResponse);

        if (captchaVerifyResult.Success == false)
        {
            return new Errors(ErrorType.RequestParsing, $"Bad Request").ToIActionResult();
        }


        Either<CreateUserRequestCommandResponse, Errors> result =
            await _createUserRequestHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadUserRequestQueryResponse, Errors> result =
            await _readUserRequestHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Update(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<UpdateUserRequestCommand, Errors> body =
            await request.ParseBodyJson<UpdateUserRequestCommand>((cmd) => cmd.Id = id, cancellationToken);
        if (body.IsRight)
            return body.Right.ToIActionResult();

        Either<UpdateUserRequestCommandResponse, Errors> result =
            await _updateUserRequestHandler.Handle(body.Left, cancellationToken);
        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
