using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;

public interface IListUsersByLaboratoryIdForWorklistFromBioHubItemHandler
{
    Task<Either<ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryResponse, Errors>> Handle(ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ListUsersByLaboratoryIdForWorklistFromBioHubItemHandler : IListUsersByLaboratoryIdForWorklistFromBioHubItemHandler
{
    private readonly ILogger<ListUsersByLaboratoryIdForWorklistFromBioHubItemHandler> _logger;
    private readonly ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryValidator _validator;
    private readonly IUserReadRepository _readRepository;

    public ListUsersByLaboratoryIdForWorklistFromBioHubItemHandler(
        ILogger<ListUsersByLaboratoryIdForWorklistFromBioHubItemHandler> logger,
        ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryValidator validator,
        IUserReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryResponse, Errors>> Handle(
        ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var excludeOnBehalfOf = !(query.UserPermissions.Contains(PermissionNames.CanReadOnBehalfOfRoles));

            IEnumerable<User> users = await _readRepository.ListUsersByLaboratoryId(query.LaboratoryId, excludeOnBehalfOf, cancellationToken);

            List<WorklistItemUserDto> worklistFromBioHubItemLaboratoryFocalPointDropDownList = new List<WorklistItemUserDto>();

            foreach (var user in users)
            {
                WorklistItemUserDto item = new WorklistItemUserDto();
                item.Id = Guid.NewGuid();
                item.UserId = user.Id;
                item.Country = user.Laboratory.Country.Name;
                item.UserName = user.FirstName + " " + user.LastName;
                item.Email = user.Email;
                item.JobTitle = user.JobTitle;
                item.Laboratory = user.Laboratory.Name;
                item.MobilePhone = user.MobilePhone;
                item.BusinessPhone = user.BusinessPhone;
                item.UserId = user.Id;
                item.LaboratoryId = user.LaboratoryId;
                item.WorklistItemId = query.WorklistFromBioHubItemId;
                worklistFromBioHubItemLaboratoryFocalPointDropDownList.Add(item);
            }


            return new(new ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryResponse(worklistFromBioHubItemLaboratoryFocalPointDropDownList));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading ListUsersByLaboratoryIdForWorklistFromBioHubItem with LaboratoryId {id}", query.LaboratoryId);
            throw;
        }
    }
}