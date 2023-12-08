using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Roles.ListRoles;

public interface IListRolesHandler
{
    Task<Either<ListRolesQueryResponse, Errors>> Handle(ListRolesQuery query, CancellationToken cancellationToken);
}

public class ListRolesHandler : IListRolesHandler
{
    private readonly ILogger<ListRolesHandler> _logger;
    private readonly ListRolesQueryValidator _validator;
    private readonly IRoleReadRepository _readRepository;

    public ListRolesHandler(
        ILogger<ListRolesHandler> logger,
        ListRolesQueryValidator validator,
        IRoleReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListRolesQueryResponse, Errors>> Handle(
        ListRolesQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var excludeOnBehalfOf = !(query.UserPermissions.Contains(PermissionNames.CanReadOnBehalfOfRoles));
            IEnumerable<Role> roles = await _readRepository.List(excludeOnBehalfOf, cancellationToken);
            var roleDtos = new List<RoleDto>();
            foreach (var role in roles)
            {
                RoleDto roleDto = new()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    RoleType = role.RoleType,
                    AddToRegistration = role.AddToRegistration,                    
                };

                roleDtos.Add(roleDto);
            }
            return new(new ListRolesQueryResponse(roleDtos));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Roles");
            throw;
        }
    }
}