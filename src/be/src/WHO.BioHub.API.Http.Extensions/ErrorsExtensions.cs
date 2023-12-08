using System.Net;
using Microsoft.AspNetCore.Mvc;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.API.Http.Extensions;

public static class ErrorsExtensions
{
    public static IActionResult ToIActionResult(this Errors error)
    {
        return error.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(error),
            ErrorType.RequestParsing => new BadRequestObjectResult(error),
            ErrorType.Validation => new BadRequestObjectResult(error),
            ErrorType.Unauthorized => new UnauthorizedResult(),
            ErrorType.Forbidden => new UnauthorizedResult(),
            _ => new ObjectResult(error) { StatusCode = (int)HttpStatusCode.InternalServerError },
        };
    }
}