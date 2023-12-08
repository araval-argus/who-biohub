using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;

public interface IListUsersHandler
{
    Task<Either<ListUsersQueryResponse, Errors>> Handle(ListUsersQuery query, CancellationToken cancellationToken);
}

public class ListUsersHandler : IListUsersHandler
{
    private readonly ILogger<ListUsersHandler> _logger;
    private readonly ListUsersQueryValidator _validator;
    private readonly IUserReadRepository _readRepository;
    private readonly IListUsersMapper _listUsersMapper;

    public ListUsersHandler(
        ILogger<ListUsersHandler> logger,
        ListUsersQueryValidator validator,
        IUserReadRepository readRepository,
        IListUsersMapper listUsersMapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _listUsersMapper = listUsersMapper;
    }

    public async Task<Either<ListUsersQueryResponse, Errors>> Handle(
        ListUsersQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<User> users;


            if (query.OnlyCouriers == true)
            {
                users = await _readRepository.ListCourierUsers(cancellationToken);
            }

            else
            {

                var excludeOnBehalfOf = !(query.UserPermissions.Contains(PermissionNames.CanReadOnBehalfOfRoles));

                switch (query.RoleType)
                {
                    case RoleType.WHO:
                        users = await _readRepository.List(excludeOnBehalfOf, cancellationToken);
                        break;
                    case RoleType.BioHubFacility:
                        users = await _readRepository.ListForBioHubFacilityUser(query.BioHubFacilityId.GetValueOrDefault(), excludeOnBehalfOf, cancellationToken);
                        break;

                    case RoleType.Laboratory:
                        users = await _readRepository.ListForLaboratoryUser(query.LaboratoryId.GetValueOrDefault(), excludeOnBehalfOf, cancellationToken);
                        break;
                    default:
                        throw new InvalidOperationException();

                }
            }

            var userViewModels = _listUsersMapper.Map(users);

            return new(new ListUsersQueryResponse(userViewModels));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all Users");
            throw;
        }
    }    
}