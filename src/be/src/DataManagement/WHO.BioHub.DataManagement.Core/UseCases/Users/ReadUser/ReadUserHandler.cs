using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;

public interface IReadUserHandler
{
    Task<Either<ReadUserQueryResponse, Errors>> Handle(ReadUserQuery query, CancellationToken cancellationToken);
}

public class ReadUserHandler : IReadUserHandler
{
    private readonly ILogger<ReadUserHandler> _logger;
    private readonly ReadUserQueryValidator _validator;
    private readonly IUserReadRepository _readRepository;
    private readonly IReadUserMapper _readUserMapper;

    public ReadUserHandler(
        ILogger<ReadUserHandler> logger,
        ReadUserQueryValidator validator,
        IUserReadRepository readRepository,
        IReadUserMapper readUserMapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _readUserMapper = readUserMapper;
    }

    public async Task<Either<ReadUserQueryResponse, Errors>> Handle(
        ReadUserQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        User user;


        try
        {
            if (query.OnlyCouriers == true)
            {
                user = await _readRepository.ReadCourierUser(query.Id, cancellationToken);
            }

            else
            {
                var excludeOnBehalfOf = !(query.UserPermissions.Contains(PermissionNames.CanReadOnBehalfOfRoles));

                switch (query.RoleType)
                {
                    case RoleType.WHO:
                        user = await _readRepository.Read(query.Id, excludeOnBehalfOf, cancellationToken);
                        break;

                    case RoleType.Laboratory:
                        user = await _readRepository.ReadForLaboratoryUser(query.Id, query.LaboratoryId.GetValueOrDefault(), excludeOnBehalfOf, cancellationToken);
                        break;

                    case RoleType.BioHubFacility:
                        user = await _readRepository.ReadForBioHubFacilityUser(query.Id, query.BioHubFacilityId.GetValueOrDefault(), excludeOnBehalfOf, cancellationToken);
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }


            if (user == null)
                return new(new Errors(ErrorType.NotFound, $"User with Id {query.Id} not found"));

            var userViewModel = _readUserMapper.Map(user);

            return new(new ReadUserQueryResponse(userViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading User with Id {id}", query.Id);
            throw;
        }
    }
}