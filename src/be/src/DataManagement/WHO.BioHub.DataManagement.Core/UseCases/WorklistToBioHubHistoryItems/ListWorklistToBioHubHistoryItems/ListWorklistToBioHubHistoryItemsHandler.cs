using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ListWorklistToBioHubHistoryItems;

public interface IListWorklistToBioHubHistoryItemsHandler
{
    Task<Either<ListWorklistToBioHubHistoryItemsQueryResponse, Errors>> Handle(ListWorklistToBioHubHistoryItemsQuery query, CancellationToken cancellationToken);
}

public class ListWorklistToBioHubHistoryItemsHandler : IListWorklistToBioHubHistoryItemsHandler
{
    private readonly ILogger<ListWorklistToBioHubHistoryItemsHandler> _logger;
    private readonly ListWorklistToBioHubHistoryItemsQueryValidator _validator;
    private readonly IWorklistToBioHubHistoryItemReadRepository _readRepository;
    private readonly IListWorklistToBioHubHistoryItemMapper _mapper;

    public ListWorklistToBioHubHistoryItemsHandler(
        ILogger<ListWorklistToBioHubHistoryItemsHandler> logger,
        ListWorklistToBioHubHistoryItemsQueryValidator validator,
        IWorklistToBioHubHistoryItemReadRepository readRepository,
        IListWorklistToBioHubHistoryItemMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ListWorklistToBioHubHistoryItemsQueryResponse, Errors>> Handle(
        ListWorklistToBioHubHistoryItemsQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            IEnumerable<WorklistToBioHubHistoryItem> worklisttobiohubhistoryitems = await _readRepository.ListByWorklistItemIdWithExtraInfo(query.WorlistToBioHubItemId, cancellationToken);

            worklisttobiohubhistoryitems = FilterItems(worklisttobiohubhistoryitems, query.RoleType, query.UserLaboratoryId, query.UserBioHubFacilityId, query.UserPermissions);

            var worklisttobiohubhistoryitemsDto = _mapper.Map(worklisttobiohubhistoryitems.ToList(), query.UserPermissions);

            return new(new ListWorklistToBioHubHistoryItemsQueryResponse(worklisttobiohubhistoryitemsDto));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving all WorklistToBioHubHistoryItems");
            throw;
        }
    }

    private IEnumerable<WorklistToBioHubHistoryItem> FilterItems(IEnumerable<WorklistToBioHubHistoryItem> worklisttobiohubitems, RoleType? roleType, Guid? LaboratoryId, Guid? BioHubFacilityId, IEnumerable<string> userPermissions)
    {
        List<WorklistToBioHubHistoryItem> filteredList = new List<WorklistToBioHubHistoryItem>();

        switch (roleType)
        {
            case RoleType.BioHubFacility:
                worklisttobiohubitems = worklisttobiohubitems.Where(x => x.RequestInitiationToBioHubFacilityId == BioHubFacilityId || x.Status <= WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval);
                break;

            case RoleType.Laboratory:
                worklisttobiohubitems = worklisttobiohubitems.Where(x => x.RequestInitiationFromLaboratoryId == LaboratoryId);
                break;
        }
        foreach (WorklistToBioHubHistoryItem worklistItem in worklisttobiohubitems)
        {
            var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(worklistItem.Status, PermissionType.Read, worklistItem.IsPast);
            if (userPermissions.Contains(requiredPermission))
            {

                filteredList.Add(worklistItem);
            }
        }
        return filteredList;
    }
}