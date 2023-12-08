using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItems;

public interface IListDashboardWorklistToBioHubItemsHandler
{
    Task<Either<ListDashboardWorklistToBioHubItemsQueryResponse, Errors>> Handle(ListDashboardWorklistToBioHubItemsQuery query, CancellationToken cancellationToken);
}

public class ListDashboardWorklistToBioHubItemsHandler : IListDashboardWorklistToBioHubItemsHandler
{
    private readonly ILogger<ListDashboardWorklistToBioHubItemsHandler> _logger;
    private readonly ListDashboardWorklistToBioHubItemsQueryValidator _validator;
    private readonly IWorklistToBioHubItemReadRepository _readRepository;
    private readonly IListDashboardWorklistToBioHubItemMapper _mapper;

    public ListDashboardWorklistToBioHubItemsHandler(
        ILogger<ListDashboardWorklistToBioHubItemsHandler> logger,
        ListDashboardWorklistToBioHubItemsQueryValidator validator,
        IWorklistToBioHubItemReadRepository readRepository,
        IListDashboardWorklistToBioHubItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListDashboardWorklistToBioHubItemsQueryResponse, Errors>> Handle(
        ListDashboardWorklistToBioHubItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<WorklistToBioHubItem> worklisttobiohubitems = await _readRepository.List(cancellationToken);

            worklisttobiohubitems = FilterItems(worklisttobiohubitems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var worklisttobiohubitemsDto = _mapper.Map(worklisttobiohubitems.ToList());

            return new(new ListDashboardWorklistToBioHubItemsQueryResponse(worklisttobiohubitemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistToBioHubItems");
            throw;
        }
    }

    private IEnumerable<WorklistToBioHubItem> FilterItems(IEnumerable<WorklistToBioHubItem> worklisttobiohubitems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistToBioHubItem> filteredList = new List<WorklistToBioHubItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklisttobiohubitems = worklisttobiohubitems.Where(x => x.RequestInitiationToBioHubFacilityId == BioHubFacilityId);
                break;

            case RoleType.Laboratory:
                worklisttobiohubitems = worklisttobiohubitems.Where(x => x.RequestInitiationFromLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistToBioHubItem worklistItem in worklisttobiohubitems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklistItem.Status, PermissionType.Update, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}