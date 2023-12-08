using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;

public interface IListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler
{
    Task<Either<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryResponse, Errors>> Handle(ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler : IListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler
{
    private readonly ILogger<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler> _logger;
    private readonly ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryValidator _validator;
    private readonly IUserReadRepository _readRepository;

    public ListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler(
        ILogger<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler> logger,
        ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryValidator validator,
        IUserReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryResponse, Errors>> Handle(
        ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var excludeOnBehalfOf = !(query.UserPermissions.Contains(PermissionNames.CanReadOnBehalfOfRoles));

            IEnumerable<User> users = await _readRepository.ListUsersByBioHubFacilityId(query.BioHubFacilityId, excludeOnBehalfOf, cancellationToken);

            List<WorklistItemUserDto> worklistFromBioHubItemBioHubFacilityUsers = new List<WorklistItemUserDto>();

            foreach (var user in users)
            {
                WorklistItemUserDto item = new WorklistItemUserDto();
                item.Id = Guid.NewGuid();
                item.UserId = user.Id;
                item.Country = user.BioHubFacility.Country.Name;
                item.UserName = user.FirstName + " " + user.LastName;
                item.Email = user.Email;
                item.JobTitle = user.JobTitle;
                item.BioHubFacility = user.BioHubFacility.Name;
                item.MobilePhone = user.MobilePhone;

                item.WorklistItemId = query.WorklistFromBioHubItemId;
                worklistFromBioHubItemBioHubFacilityUsers.Add(item);
            }


            return new(new ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryResponse(worklistFromBioHubItemBioHubFacilityUsers));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading ListUsersByBioHubFacilityIdForWorklistFromBioHubItem with BioHubFacilityId {id}", query.BioHubFacilityId);
            throw;
        }
    }
}