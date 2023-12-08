using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;

public interface IListUsersByLaboratoryIdForWorklistToBioHubItemHandler
{
    Task<Either<ListUsersByLaboratoryIdForWorklistToBioHubItemQueryResponse, Errors>> Handle(ListUsersByLaboratoryIdForWorklistToBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ListUsersByLaboratoryIdForWorklistToBioHubItemHandler : IListUsersByLaboratoryIdForWorklistToBioHubItemHandler
{
    private readonly ILogger<ListUsersByLaboratoryIdForWorklistToBioHubItemHandler> _logger;
    private readonly ListUsersByLaboratoryIdForWorklistToBioHubItemQueryValidator _validator;
    private readonly IUserReadRepository _readRepository;

    public ListUsersByLaboratoryIdForWorklistToBioHubItemHandler(
        ILogger<ListUsersByLaboratoryIdForWorklistToBioHubItemHandler> logger,
        ListUsersByLaboratoryIdForWorklistToBioHubItemQueryValidator validator,
        IUserReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListUsersByLaboratoryIdForWorklistToBioHubItemQueryResponse, Errors>> Handle(
        ListUsersByLaboratoryIdForWorklistToBioHubItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            var excludeOnBehalfOf = !(query.UserPermissions.Contains(PermissionNames.CanReadOnBehalfOfRoles));

            IEnumerable<User> users = await _readRepository.ListUsersByLaboratoryId(query.LaboratoryId, excludeOnBehalfOf, cancellationToken);

            List<WorklistItemUserDto> worklistToBioHubItemLaboratoryFocalPointDropDownList = new List<WorklistItemUserDto>();

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
                item.WorklistItemId = query.WorklistToBioHubItemId;
                worklistToBioHubItemLaboratoryFocalPointDropDownList.Add(item);
            }


            return new(new ListUsersByLaboratoryIdForWorklistToBioHubItemQueryResponse(worklistToBioHubItemLaboratoryFocalPointDropDownList));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading ListUsersByLaboratoryIdForWorklistToBioHubItem with LaboratoryId {id}", query.LaboratoryId);
            throw;
        }
    }
}