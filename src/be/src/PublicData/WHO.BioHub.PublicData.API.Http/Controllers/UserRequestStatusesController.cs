using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IUserRequestStatusesController
{

    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> ReadByStatus(HttpRequest request, UserRegistrationStatus status, CancellationToken cancellationToken);
}

public class UserRequestStatusesController : ControllerBase, IUserRequestStatusesController
{
    private readonly IReadUserRequestStatusHandler _readUserRequestStatusHandler;
    private readonly IReadUserRequestStatusByStatusHandler _readUserRequestStatusByStatusHandler;

    public UserRequestStatusesController(
        IReadUserRequestStatusHandler readUserRequestStatusHandler,
        IReadUserRequestStatusByStatusHandler readUserRequestStatusByStatusHandler
        )
    {

        _readUserRequestStatusHandler = readUserRequestStatusHandler;
        _readUserRequestStatusByStatusHandler = readUserRequestStatusByStatusHandler;

    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadUserRequestStatusQueryResponse, Errors> result =
            await _readUserRequestStatusHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> ReadByStatus(HttpRequest request, UserRegistrationStatus status, CancellationToken cancellationToken)
    {
        Either<ReadUserRequestStatusByStatusQueryResponse, Errors> result =
            await _readUserRequestStatusByStatusHandler.Handle(new(Status: status), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

}
