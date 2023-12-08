using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListCourierUsers;

public interface IListCourierUsersForWorklistFromBioHubItemHandler
{
    Task<Either<ListCourierUsersForWorklistFromBioHubItemQueryResponse, Errors>> Handle(ListCourierUsersForWorklistFromBioHubItemQuery query, CancellationToken cancellationToken);
}

public class ListCourierUsersForWorklistFromBioHubItemHandler : IListCourierUsersForWorklistFromBioHubItemHandler
{
    private readonly ILogger<ListCourierUsersForWorklistFromBioHubItemHandler> _logger;
    private readonly ListCourierUsersForWorklistFromBioHubItemQueryValidator _validator;
    private readonly IUserReadRepository _readRepository;

    public ListCourierUsersForWorklistFromBioHubItemHandler(
        ILogger<ListCourierUsersForWorklistFromBioHubItemHandler> logger,
        ListCourierUsersForWorklistFromBioHubItemQueryValidator validator,
        IUserReadRepository readRepository)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
    }

    public async Task<Either<ListCourierUsersForWorklistFromBioHubItemQueryResponse, Errors>> Handle(
        ListCourierUsersForWorklistFromBioHubItemQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<User> users = await _readRepository.ListCourierUsersForWorklist(cancellationToken);

            List<WorklistItemUserDto> worklistFromBioHubItemCourierUsersDropDownList = new List<WorklistItemUserDto>();

            foreach (var user in users)
            {
                WorklistItemUserDto item = new WorklistItemUserDto();
                item.Id = Guid.NewGuid();
                item.UserId = user.Id;
                item.Country = user.Courier.Country.Name;
                item.UserName = user.FirstName + " " + user.LastName;
                item.Email = user.Email;
                item.JobTitle = user.JobTitle;
                item.MobilePhone = user.MobilePhone;
                item.BusinessPhone = user.BusinessPhone;
                item.UserId = user.Id;
                item.CourierId = user.CourierId;
                item.WorklistItemId = query.WorklistFromBioHubItemId;
                worklistFromBioHubItemCourierUsersDropDownList.Add(item);
            }


            return new(new ListCourierUsersForWorklistFromBioHubItemQueryResponse(worklistFromBioHubItemCourierUsersDropDownList));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading ListCourierUsersForWorklistFromBioHubItem");
            throw;
        }
    }
}