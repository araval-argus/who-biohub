using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItems;

public interface IListDashboardWorklistFromBioHubItemsHandler
{
    Task<Either<ListDashboardWorklistFromBioHubItemsQueryResponse, Errors>> Handle(ListDashboardWorklistFromBioHubItemsQuery query, CancellationToken cancellationToken);
}

public class ListDashboardWorklistFromBioHubItemsHandler : IListDashboardWorklistFromBioHubItemsHandler
{
    private readonly ILogger<ListDashboardWorklistFromBioHubItemsHandler> _logger;
    private readonly ListDashboardWorklistFromBioHubItemsQueryValidator _validator;
    private readonly IWorklistFromBioHubItemReadRepository _readRepository;
    private readonly IListDashboardWorklistFromBioHubItemMapper _mapper;

    public ListDashboardWorklistFromBioHubItemsHandler(
        ILogger<ListDashboardWorklistFromBioHubItemsHandler> logger,
        ListDashboardWorklistFromBioHubItemsQueryValidator validator,
        IWorklistFromBioHubItemReadRepository readRepository,
        IListDashboardWorklistFromBioHubItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListDashboardWorklistFromBioHubItemsQueryResponse, Errors>> Handle(
        ListDashboardWorklistFromBioHubItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<WorklistFromBioHubItem> worklistfrombiohubitems = await _readRepository.List(cancellationToken);

            worklistfrombiohubitems = FilterItems(worklistfrombiohubitems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var worklistfrombiohubitemsDto = _mapper.Map(worklistfrombiohubitems.ToList());

            return new(new ListDashboardWorklistFromBioHubItemsQueryResponse(worklistfrombiohubitemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistFromBioHubItems");
            throw;
        }
    }

    private IEnumerable<WorklistFromBioHubItem> FilterItems(IEnumerable<WorklistFromBioHubItem> worklistfrombiohubitems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistFromBioHubItem> filteredList = new List<WorklistFromBioHubItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklistfrombiohubitems = worklistfrombiohubitems.Where(x => x.RequestInitiationFromBioHubFacilityId == BioHubFacilityId);
                break;

            case RoleType.Laboratory:
                worklistfrombiohubitems = worklistfrombiohubitems.Where(x => x.RequestInitiationToLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistFromBioHubItem worklistItem in worklistfrombiohubitems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistItem.Status, PermissionType.Update, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}