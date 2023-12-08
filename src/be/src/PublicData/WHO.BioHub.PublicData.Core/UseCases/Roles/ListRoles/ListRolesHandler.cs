using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Roles.ListRoles;

public interface IListRolesHandler
{
    Task<Either<ListRolesQueryResponse, Errors>> Handle(ListRolesQuery query, CancellationToken cancellationToken);
}

public class ListRolesHandler : IListRolesHandler
{
    private readonly ILogger<ListRolesHandler> _logger;
    private readonly ListRolesQueryValidator _validator;
    private readonly IRolePublicReadRepository _readRepository;

    public ListRolesHandler(
        ILogger<ListRolesHandler> logger,
        ListRolesQueryValidator validator,
        IRolePublicReadRepository readRepository)
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
            IEnumerable<Role> roles = await _readRepository.List(cancellationToken);

            var rolesView = new List<RolePublicViewModel>();

            foreach (var role in roles)
            {
                var roleView = new RolePublicViewModel();
                roleView.Id = role.Id;
                roleView.Name = role.PublicName;
                //roleView.Description = role.Description;
                rolesView.Add(roleView);
            }

            return new(new ListRolesQueryResponse(rolesView));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Roles");
            throw;
        }
    }
}