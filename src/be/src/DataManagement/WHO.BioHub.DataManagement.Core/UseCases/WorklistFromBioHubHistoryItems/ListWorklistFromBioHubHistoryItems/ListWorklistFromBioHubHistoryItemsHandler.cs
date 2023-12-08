using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ReadWorklistFromBioHubHistoryItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ListWorklistFromBioHubHistoryItems;

public interface IListWorklistFromBioHubHistoryItemsHandler
{
    Task<Either<ListWorklistFromBioHubHistoryItemsQueryResponse, Errors>> Handle(ListWorklistFromBioHubHistoryItemsQuery query, CancellationToken cancellationToken);
}

public class ListWorklistFromBioHubHistoryItemsHandler : IListWorklistFromBioHubHistoryItemsHandler
{
    private readonly ILogger<ListWorklistFromBioHubHistoryItemsHandler> _logger;
    private readonly ListWorklistFromBioHubHistoryItemsQueryValidator _validator;
    private readonly IWorklistFromBioHubHistoryItemReadRepository _readRepository;
    private readonly IListWorklistFromBioHubHistoryItemMapper _mapper;

    public ListWorklistFromBioHubHistoryItemsHandler(
        ILogger<ListWorklistFromBioHubHistoryItemsHandler> logger,
        ListWorklistFromBioHubHistoryItemsQueryValidator validator,
        IWorklistFromBioHubHistoryItemReadRepository readRepository,
        IListWorklistFromBioHubHistoryItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListWorklistFromBioHubHistoryItemsQueryResponse, Errors>> Handle(
        ListWorklistFromBioHubHistoryItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<WorklistFromBioHubHistoryItem> worklistfrombiohubhistoryitems = await _readRepository.ListByWorklistItemIdWithExtraInfo(query.WorlistFromBioHubItemId, cancellationToken);

            worklistfrombiohubhistoryitems = FilterItems(worklistfrombiohubhistoryitems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var worklistfrombiohubhistoryitemsDto = _mapper.Map(worklistfrombiohubhistoryitems.ToList(), query.UserPermissions);

            return new(new ListWorklistFromBioHubHistoryItemsQueryResponse(worklistfrombiohubhistoryitemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistFromBioHubHistoryItems");
            throw;
        }
    }

    private IEnumerable<WorklistFromBioHubHistoryItem> FilterItems(IEnumerable<WorklistFromBioHubHistoryItem> worklistfrombiohubhistoryitems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistFromBioHubHistoryItem> filteredList = new List<WorklistFromBioHubHistoryItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklistfrombiohubhistoryitems = worklistfrombiohubhistoryitems.Where(x => x.RequestInitiationFromBioHubFacilityId == BioHubFacilityId || x.Status <= WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval);
                break;

            case RoleType.Laboratory:
                worklistfrombiohubhistoryitems = worklistfrombiohubhistoryitems.Where(x => x.RequestInitiationToLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistFromBioHubHistoryItem worklistItem in worklistfrombiohubhistoryitems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {
                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}