using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WHO.BioHub.PublicData.Core.UseCases.Roles.ListRoles;
using WHO.BioHub.PublicData.Core.UseCases.Roles.ReadRole;
using WHO.BioHub.API.Http.Extensions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.PublicData.API.Http.Controllers;

public interface IRolesController
{
    Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken);
    Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken);
}

public class RolesController : ControllerBase, IRolesController
{
    private readonly IReadRoleHandler _readRoleHandler;
    private readonly IListRolesHandler _listRolesHandler;

    public RolesController(
        IReadRoleHandler readRoleHandler,
        IListRolesHandler listRolesHandler)
    {
        _readRoleHandler = readRoleHandler;
        _listRolesHandler = listRolesHandler;
    }

    public async Task<IActionResult> List(HttpRequest request, CancellationToken cancellationToken)
    {
        Either<ListRolesQueryResponse, Errors> result =
            await _listRolesHandler.Handle(new(), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }

    public async Task<IActionResult> Read(HttpRequest request, Guid id, CancellationToken cancellationToken)
    {
        Either<ReadRoleQueryResponse, Errors> result =
            await _readRoleHandler.Handle(new(Id: id), cancellationToken);

        if (result.IsRight)
            return result.Right.ToIActionResult();

        return new OkObjectResult(result.Left);
    }
}
